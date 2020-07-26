using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestWebSite.Services
{
    public class BGWorker : IHostedService, IDisposable
    {
        Timer timer;

        readonly IServiceScopeFactory scopeFactory;
        static readonly object obj = new object();

        public BGWorker(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        void DoWork(object state)
        {
            lock (obj)
            {
                try
                {
                    using var scope = scopeFactory.CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<Database.Context>();

                    context.Ticks.Add(new Database.Models.Ticks());
                    context.SaveChanges();
                }
                catch { }
            }
        }
    }
}
