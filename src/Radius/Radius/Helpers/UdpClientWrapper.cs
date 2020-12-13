using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Radius.Helpers
{
    public class UdpClientWrapper
    {
        public Socket Client => client.Client;

        private readonly UdpClient client;

        public UdpClientWrapper(IPEndPoint localEndpoint)
        {
            client = new UdpClient(localEndpoint);
        }

        public void Close()
        {
            client.Close();
        }

        public void Send(byte[] content, int length, IPEndPoint remoteEndpoint)
        {
            client.Send(content, length, remoteEndpoint);
        }

        public Task<int> SendAsync(byte[] content, int length, IPEndPoint remoteEndpoint)
        {
            return client.SendAsync(content, length, remoteEndpoint);
        }

        public Task<UdpReceiveResult> ReceiveAsync()
        {
            return client.ReceiveAsync();
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
