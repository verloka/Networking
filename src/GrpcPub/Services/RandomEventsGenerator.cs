using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcPub.Services
{
    public class RandomEventsGenerator : IHostedService, IDisposable
    {
        const int REQUEST_INTERVAL = 5_000;
        Timer Timer;
        Random Rnd = new((int)DateTime.Now.Ticks);

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Timer = new Timer(TimerElapsed);
            Timer.Change(REQUEST_INTERVAL * 3, Timeout.Infinite);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Timer?.Change(Timeout.Infinite, Timeout.Infinite);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Timer.Change(Timeout.Infinite, Timeout.Infinite);
            Timer?.Dispose();
        }

        void TimerElapsed(object state)
        {
            Timer.Change(Timeout.Infinite, Timeout.Infinite);

            EventType type = (EventType)Rnd.Next(0, 5);
            string data = DateTime.Now.Ticks.ToString();

            PublisherService.PostServiceEvent(type, data);

            Debug.WriteLine($"Posted event type: {type} with data: {data}");

            Timer.Change(REQUEST_INTERVAL, Timeout.Infinite);
        }
    }
}
