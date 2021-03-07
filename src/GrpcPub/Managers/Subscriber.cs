using Grpc.Core;
using GrpcPub.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcPub.Managers
{
    public class Subscriber
    {
        const int REQUEST_INTERVAL = 10_000;

        public event PostEnvetHandler OnEvent;
        readonly Publisher.PublisherClient Client;
        SubscriptionRequest Subscription;
        CancellationTokenSource SubscriptionCancel;
        readonly Timer Timer;

        public Subscriber(Publisher.PublisherClient Client)
        {
            Timer = new Timer(TimerElapsed);
            this.Client = Client;
        }

        public void Subscribe(string Id, EventType Type)
        {
            Subscription = new SubscriptionRequest() { Id = Id, Type = Type };
            SubscriptionCancel = new CancellationTokenSource();
            SubscribeToEvent(SubscriptionCancel.Token).ConfigureAwait(false).GetAwaiter();
            Timer.Change(REQUEST_INTERVAL, Timeout.Infinite);
        }

        public void Unsubscribe()
        {
            Timer.Change(Timeout.Infinite, Timeout.Infinite);
            Timer.Dispose();
            Client.Unsubscribe(new UnsubscriptionRequest { Id = Subscription.Id });
        }

        async Task SubscribeToEvent(CancellationToken cancellation)
        {
            try
            {
                using var call = Client.Subscribe(Subscription);
                var responseReaderTask = Task.Run(async () =>
                {
                    while (await call.ResponseStream.MoveNext())
                    {
                        if (call.ResponseStream.Current.Type == Subscription.Type)
                            OnEvent?.Invoke(this, new PostEnvetArgs(call.ResponseStream.Current));
                    }
                }, cancellation);

                await responseReaderTask;
            }
            catch (OperationCanceledException) { }
            catch { throw; }
        }

        void TimerElapsed(object state)
        {
            Timer.Change(Timeout.Infinite, Timeout.Infinite);

            bool resubscribe = false;

            try
            {
                var result = Client.IsSubscribed(new IsSubscribedRequest { Id = Subscription.Id }, deadline: DateTime.UtcNow.AddSeconds(15));
                resubscribe = !result.Subscribed;
            }
            catch
            {
                resubscribe = true;

            }

            if (resubscribe)
            {
                Console.WriteLine("Try to resubscribe...");

                SubscriptionCancel.Cancel();
                SubscriptionCancel = new CancellationTokenSource();
                SubscribeToEvent(SubscriptionCancel.Token).ConfigureAwait(false).GetAwaiter();
            }

            Timer.Change(REQUEST_INTERVAL, Timeout.Infinite);
        }
    }
}
