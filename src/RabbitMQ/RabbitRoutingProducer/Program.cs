using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitRoutingProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RabbitMQ Publisher (with rout)");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "main_logs", type: ExchangeType.Direct);

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
                        var body = Encoding.UTF8.GetBytes(str);
                        channel.BasicPublish(exchange: "main_logs",
                                     routingKey: str.Split(" ")[0],
                                     basicProperties: null,
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
