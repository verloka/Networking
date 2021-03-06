﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitRoutingConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RabbitMQ Subscriber (with rout)");

            Console.Write("Subscribe to: ");
            string rout = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(rout))
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.ExchangeDeclare(exchange: "main_logs", type: ExchangeType.Direct);
                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName, exchange: "main_logs", routingKey: rout);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += ConsumerReceived;

                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine("Press any key to exit...");
                Console.Read();
            }
        }

        private static void ConsumerReceived(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Received: {message}");
        }
    }
}
