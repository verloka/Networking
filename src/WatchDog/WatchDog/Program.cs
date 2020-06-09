using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WatchDog
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string HEALTHY_STATUS = "Healthy";

        static async Task Main(string[] args)
        {
            while (true)
            {
                var healthRequest = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44385/health");
                var healthResponse = await client.SendAsync(healthRequest);
                var healthStatus = await healthResponse.Content.ReadAsStringAsync();

                if (healthStatus != HEALTHY_STATUS)
                {
                    Console.WriteLine($"{ DateTime.Now.ToLocalTime().ToLongTimeString()} : Unhealthy API. Restarting Dependency");
                    var resetRequest = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44385/data/reset");
                    _ = await client.SendAsync(resetRequest);
                }
                else
                {
                    Console.WriteLine($"{DateTime.Now.ToLocalTime().ToLongTimeString()} :  Healthy API");
                }

                Thread.Sleep(10000);
            }
        }
    }
}
