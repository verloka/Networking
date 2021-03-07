using Grpc.Net.Client;
using GrpcPub;
using GrpcPub.Managers;
using System;

namespace GrpcSub
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select an event to subscribe:");
            Console.WriteLine("1) Unknown");
            Console.WriteLine("2) Message");
            Console.WriteLine("3) Time update");
            Console.WriteLine("4) New customer");
            Console.WriteLine("5) Unsubscribe");

            Console.Write("Event: ");
            string value = Console.ReadLine();
            if(int.TryParse(value, out int t) && t >= 1 && t <= 5)
            {
                GrpcChannel Channel = GrpcChannel.ForAddress("http://localhost:5000");
                Publisher.PublisherClient Client = new(Channel);
                Subscriber Sub = new(Client);
                Sub.OnEvent += SubOnEvent;

                EventType Type = (EventType)(t - 1);
                string Id = Guid.NewGuid().ToString();

                Sub.Subscribe(Id, Type);

                Console.WriteLine($"Assigned a new id: {Id}, subscribed to {Type}");
                Console.WriteLine("Press any key to unsubscribe...");
                Console.ReadLine();

                Sub.Unsubscribe();
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        private static void SubOnEvent(object sender, GrpcPub.Events.PostEnvetArgs e)
        {
            if (e.Event.Type == EventType.Unsubscribe)
                Environment.Exit(-1);

            Console.WriteLine($"Event type: {e.Event.Type}, details: {e.Event.DetailJson}");
        }
    }
}
