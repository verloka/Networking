using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var client = new UdpClient(45678))
            {
                IPEndPoint remoteEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 34567);

                var incomingMessage = await client.ReceiveAsync();
                var message = Encoding.UTF8.GetString(incomingMessage.Buffer);
                Console.WriteLine(message);


                var responseString = $"Packet received from client: {message}";
                byte[] response = Encoding.UTF8.GetBytes(responseString);
                await client.SendAsync(response, response.Length, remoteEndpoint);
            }

            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }
    }
}
