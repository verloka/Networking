using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace RabbitWorkerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "tasks_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            //one message per time
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ConsumerReceived;

            channel.BasicConsume(queue: "tasks_queue",
                                 autoAck: false,
                                 consumer: consumer);

            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }

        private static void ConsumerReceived(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            //emulating a heavy task
            int dots = message.Split('.').Length - 1;
            Thread.Sleep(dots * 1000);

            Console.WriteLine($"Received: {message}");

            //if client downed while processing a message - message will be re-delivered
            ((EventingBasicConsumer)sender).Model.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
        }
    }
}
