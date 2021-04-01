using System;
using System.ServiceModel;

namespace WCFServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(ServiceInterface.HelloService)))
            {
                host.Open();
                Console.WriteLine($"Host started at {DateTime.Now}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
            }
        }
    }
}
