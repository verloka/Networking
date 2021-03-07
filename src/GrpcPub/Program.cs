using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace GrpcPub
{
    public class Program
    {
        internal static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
