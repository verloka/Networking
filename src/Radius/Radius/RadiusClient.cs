using Radius.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Radius
{
    public class RadiusClient
    {
        private readonly IPEndPoint LocalEndpoint;
        private readonly UdpClient Client;
        private readonly IRadiusPacketParser PacketParser;
        private readonly ConcurrentDictionary<(byte identifier, IPEndPoint remoteEndpoint), TaskCompletionSource<UdpReceiveResult>> _pendingRequests = new ConcurrentDictionary<(byte, IPEndPoint), TaskCompletionSource<UdpReceiveResult>>();

        /// <summary>
        /// Create a radius client which sends and receives responses on localEndpoint
        /// </summary>
        /// <param name="localEndpoint"></param>
        /// <param name="dictionary"></param>
        public RadiusClient(IPEndPoint localEndpoint, IRadiusPacketParser radiusPacketParser)
        {
            LocalEndpoint = localEndpoint;
            PacketParser = radiusPacketParser;
            Client = new UdpClient(LocalEndpoint);
            _ = StartReceiveLoopAsync();
        }

        /// <summary>
        /// Send a packet with specified timeout
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="remoteEndpoint"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public async Task<IRadiusPacket> SendPacketAsync(IRadiusPacket packet, IPEndPoint remoteEndpoint, TimeSpan timeout)
        {
            var packetBytes = PacketParser.GetBytes(packet);
            var responseTaskCS = new TaskCompletionSource<UdpReceiveResult>();
            if (_pendingRequests.TryAdd((packet.Identifier, remoteEndpoint), responseTaskCS))
            {
                await Client.SendAsync(packetBytes, packetBytes.Length, remoteEndpoint);
                var completedTask = await Task.WhenAny(responseTaskCS.Task, Task.Delay(timeout));
                if (completedTask == responseTaskCS.Task)
                {
                    return PacketParser.Parse(responseTaskCS.Task.Result.Buffer, packet.SharedSecret);
                }
                throw new InvalidOperationException($"Receive response for id {packet.Identifier} timed out after {timeout}");
            }
            throw new InvalidOperationException($"There is already a pending receive with id {packet.Identifier}");
        }

        /// <summary>
        /// Send a packet with default timeout of 3 seconds
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="remoteEndpoint"></param>
        /// <returns></returns>
        public async Task<IRadiusPacket> SendPacketAsync(IRadiusPacket packet, IPEndPoint remoteEndpoint)
        {
            return await SendPacketAsync(packet, remoteEndpoint, TimeSpan.FromSeconds(3));
        }

        /// <summary>
        /// Receive packets in a loop and complete tasks based on identifier
        /// </summary>
        /// <returns></returns>
        async Task StartReceiveLoopAsync()
        {
            while (true)    // Maybe this should be started and stopped when there are pending responses
            {
                try
                {
                    var response = await Client.ReceiveAsync();
                    if (_pendingRequests.TryRemove((response.Buffer[1], response.RemoteEndPoint), out var taskCS))
                    {
                        taskCS.SetResult(response);
                    }
                }
                catch (ObjectDisposedException)
                {
                    // This is thrown when udpclient is disposed, can be safely ignored
                    return;
                }
            }
        }

        public void Dispose()
        {
            Client?.Dispose();
        }
    }
}
