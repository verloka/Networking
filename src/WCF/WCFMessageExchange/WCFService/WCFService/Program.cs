using System;
using System.ServiceModel;

namespace WCFService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(HelloService)))
            {
                host.Open();
                Console.WriteLine($"Host started at {DateTime.Now}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
            }
        }
    }
}
