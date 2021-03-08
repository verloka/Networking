using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitWorkerProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RabbitMQ Worker Producer");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "tasks_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            int i = 1;

            while (true)
            {
                Console.WriteLine("To exit type 'exit'");
                Console.Write("Send: ");
                string str = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(str))
                {
                    Console.Clear();

                    if (str != "exit")
                    {
                        var body = Encoding.UTF8.GetBytes($"{i++}) {str}");
                        channel.BasicPublish(exchange: "",
                                     routingKey: "tasks_queue",
                                     basicProperties: properties,
                                     body: body);

                        Console.WriteLine($"Sended: {str}");
                    }
                    else
                    {
                        break;
                    }
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }
    }
}
