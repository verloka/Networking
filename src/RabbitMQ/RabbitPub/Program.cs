using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitPub
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RabbitMQ Publisher");

            Console.Write("Publish to: ");
            string exchange = Console.ReadLine();

            if(!string.IsNullOrWhiteSpace(exchange))
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

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
                            channel.BasicPublish(exchange: exchange,
                                         routingKey: "",
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
}
