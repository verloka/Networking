using System;
using System.Net;

namespace DNSTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var entry1 = Dns.GetHostEntry("verloka.com");
            Console.WriteLine($"{entry1.HostName}:");
            foreach (var ip in entry1.AddressList)
                Console.WriteLine(ip);

            var entry2 = Dns.GetHostEntry("1.1.1.1");
            Console.WriteLine($"{entry2.HostName}:");
            foreach (var ip in entry2.AddressList)
                Console.WriteLine(ip);

            Console.WriteLine("Press any key to exit.");
            Console.Read();
        }
    }
}
