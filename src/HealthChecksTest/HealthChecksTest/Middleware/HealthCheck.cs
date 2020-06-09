using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HealthChecksTest.Middleware
{
    public class HealthCheck : IHealthCheck
    {
        readonly int DEGRADING_THRESHOLD = 2000;
        readonly int UNHEALTHY_THRESHOLD = 5000;

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var httpClient = HttpClientFactory.Create();
            httpClient.BaseAddress = new Uri("https://localhost:44385");
            var request = new HttpRequestMessage(HttpMethod.Get, "/data");
            Stopwatch sw = Stopwatch.StartNew();
            _ = await httpClient.SendAsync(request);
            sw.Stop();
            var responseTime = sw.ElapsedMilliseconds;
            if (responseTime < DEGRADING_THRESHOLD)
                return HealthCheckResult.Healthy("The dependent system is performing within acceptable parameters");
            else if (responseTime < UNHEALTHY_THRESHOLD)
                return HealthCheckResult.Degraded("The dependent system is degrading and likely to fail soon");
            else
                return HealthCheckResult.Unhealthy("The dependent system is unacceptably degraded. Restart.");
        }
    }
}
