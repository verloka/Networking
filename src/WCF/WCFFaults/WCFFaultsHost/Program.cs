using System;
using System.ServiceModel;
using WCFFaults;

namespace WCFFaultsHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host1 = new ServiceHost(typeof(SimpleCalculator)))
            using (var host2 = new ServiceHost(typeof(ComplexCalculator)))
            {
                host1.Open();
                host2.Open();
                Console.WriteLine($"Hosts started at {DateTime.Now}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
            }
        }
    }
}
