using Grpc.Core;
using GrpcPub.Managers;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace GrpcPub
{
    internal class PublisherService : Publisher.PublisherBase
    {
        static SubscribersMap SubscriberWritersMap { get; set; } = new SubscribersMap();
        static BufferBlock<Event> EventsBuffer { get; set; } = new BufferBlock<Event>();

        public override async Task Subscribe(SubscriptionRequest request, IServerStreamWriter<Event> responseStream, ServerCallContext context)
        {
            SubscriberWritersMap.SetStream(request.Id, request.Type, responseStream);

            while (SubscriberWritersMap.Count > 0)
            {
                var @event = await EventsBuffer.ReceiveAsync();
                foreach (var x in SubscriberWritersMap[request.Type])
                    try { await x.WriteAsync(@event); }
                    catch { SubscriberWritersMap.Remove(request.Id); }
            }
        }

        public override Task<UnsubscriptionRequest> Unsubscribe(UnsubscriptionRequest request, ServerCallContext context)
        {
            SubscriberWritersMap.Remove(request.Id);
            return Task.FromResult(request);
        }

        public override Task<IsSubscribedResponse> IsSubscribed(IsSubscribedRequest request, ServerCallContext context)
        {
            return Task.FromResult(new IsSubscribedResponse { Subscribed = SubscriberWritersMap.ContainsKey(request.Id) });
        }

        public static void PostServiceEvent(EventType Type, string DetailJson) => EventsBuffer.Post(new Event { Type = Type, DetailJson = DetailJson });
    }
}
