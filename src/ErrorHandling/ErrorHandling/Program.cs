using System;
using System.Threading;
using System.Threading.Tasks;

namespace ErrorHandling
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await AsyncRequest.Make("tttggoogle.com");

            for (var i = 0; i < 24; i++)
            {
                Console.WriteLine($"Polly Demo Attempt {i}");
                Console.WriteLine("-------------");
                PollyHandling.ExecuteRemoteLookupWithPolly();
                Console.WriteLine("-------------");
                Thread.Sleep(3000);
            }


            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }
    }
}
