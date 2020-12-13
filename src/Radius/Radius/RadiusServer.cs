using Radius.Enums;
using Radius.Handlers;
using Radius.Helpers;
using Radius.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Radius
{
    public sealed class RadiusServer : IDisposable
    {
        public bool Running { get; private set; }

        private UdpClientWrapper Server;

        private readonly IPEndPoint LocalEndpoint;
        private readonly RadiusPacketHandlerRepository PacketHandlerRepository;
        private readonly IRadiusPacketParser RadiusPacketParser;
        private readonly RadiusServerType ServerType;
        private int HandlerCount = 0;

        /// <summary>
        /// Create a new server on endpoint with packet handler repository
        /// </summary>
        /// <param name="localEndpoint"></param>
        /// <param name="radiusPacketParser"></param>
        /// <param name="serverType"></param>
        /// <param name="packetHandlerRepository"></param>
        public RadiusServer(IPEndPoint localEndpoint, IRadiusPacketParser radiusPacketParser, RadiusServerType serverType, RadiusPacketHandlerRepository packetHandlerRepository)
        {
            LocalEndpoint = localEndpoint;
            RadiusPacketParser = radiusPacketParser;
            ServerType = serverType;
            PacketHandlerRepository = packetHandlerRepository;
        }

        /// <summary>
        /// Start listening for requests
        /// </summary>
        public void Start()
        {
            if (!Running)
            {
                Server = new UdpClientWrapper(LocalEndpoint);
                Running = true;

                //TODO log output here
                //$"Starting Radius server on {LocalEndpoint}"

                _ = StartReceiveLoopAsync();

                //TODO log output here
                //"Server started"
            }
            else
            {
                //TODO log output here
                //"Server already started"
            }
        }

        /// <summary>
        /// Stop listening
        /// </summary>
        public void Stop()
        {
            if (Running)
            {
                //TODO log output here
                //"Stopping server"

                Running = false;
                Server?.Dispose();

                //TODO log output here
                //"Stopped"
            }
            else
            {
                //TODO log output here
                //"Server already stopped"
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            foreach (IPacketHandler handler in PacketHandlerRepository)
                handler.Dispose();

            Server?.Dispose();
        }

        /// <summary>
        /// Parses a packet and gets a response packet from the handler
        /// </summary>
        /// <param name="packetHandler"></param>
        /// <param name="sharedSecret"></param>
        /// <param name="packetBytes"></param>
        /// <param name="remoteEndpoint"></param>
        /// <returns></returns>
        internal IRadiusPacket GetResponsePacket(IPacketHandler packetHandler, string sharedSecret, byte[] packetBytes, IPEndPoint remoteEndpoint)
        {
            var requestPacket = RadiusPacketParser.Parse(packetBytes, Encoding.UTF8.GetBytes(sharedSecret));

            //TODO log output here
            //$"Received {requestPacket.Code} from {remoteEndpoint} Id={requestPacket.Identifier}"

            //if logger in debug mode
            //DumpPacket(requestPacket);

            //TODO log output here
            //packetBytes.ToHexString()

            // Handle status server requests in server outside packet handler
            if (requestPacket.Code == PacketCode.StatusServer)
            {
                var responseCode = ServerType == RadiusServerType.Authentication ? PacketCode.AccessAccept : PacketCode.AccountingResponse;

                //TODO log output here
                //$"Sending {responseCode} for StatusServer request from {remoteEndpoint}"

                return requestPacket.CreateResponsePacket(responseCode);
            }

            //TODO log output here
            //$"Handling packet for remote ip {remoteEndpoint.Address} with {packetHandler.GetType()}"

            var sw = Stopwatch.StartNew();
            var responsePacket = packetHandler.HandlePacket(requestPacket);
            sw.Stop();

            //TODO log output here
            //$"{remoteEndpoint} Id={responsePacket.Identifier}, Received {responsePacket.Code} from handler in {sw.ElapsedMilliseconds}ms"

            //TODO log output here
            //if (sw.ElapsedMilliseconds >= 5000)
            //$"Slow response for Id {responsePacket.Identifier}, check logs"

            if (requestPacket.Attributes.ContainsKey("Proxy-State"))
            {
                responsePacket.Attributes.Add("Proxy-State", requestPacket.Attributes.SingleOrDefault(o => o.Key == "Proxy-State").Value);
            }

            return responsePacket;
        }

        /// <summary>
        /// Start the loop used for receiving packets
        /// </summary>
        /// <returns></returns>
        async Task StartReceiveLoopAsync()
        {
            while (Running)
            {
                try
                {
                    var response = await Server.ReceiveAsync();
                    var task = Task.Factory.StartNew(() => HandlePacket(response.RemoteEndPoint, response.Buffer), TaskCreationOptions.LongRunning);
                }
                catch (ObjectDisposedException) { } // This is thrown when udpclient is disposed, can be safely ignored
                catch (Exception ex)
                {
                    //TODO log output here
                    //"Something went wrong receiving packet"
                }
            }
        }

        /// <summary>
        /// Used to handle the packets asynchronously
        /// </summary>
        /// <param name="remoteEndpoint"></param>
        /// <param name="packetBytes"></param>
        void HandlePacket(IPEndPoint remoteEndpoint, byte[] packetBytes)
        {
            try
            {
                //TODO log output here
                //$"Received packet from {remoteEndpoint}, Concurrent handlers count: {Interlocked.Increment(ref HandlerCount)}"

                if (PacketHandlerRepository.TryGetHandler(remoteEndpoint.Address, out var handler))
                {
                    var responsePacket = GetResponsePacket(handler.packetHandler, handler.sharedSecret, packetBytes, remoteEndpoint);
                    if (responsePacket != null)
                    {
                        SendResponsePacket(responsePacket, remoteEndpoint);
                    }
                }
                else
                {
                    //TODO log output here
                    //$"No packet handler found for remote ip {remoteEndpoint}"

                    var packet = RadiusPacketParser.Parse(packetBytes, Encoding.UTF8.GetBytes("wut"));
                    DumpPacket(packet);
                }
            }
            catch (Exception ex) when (ex is ArgumentException || ex is OverflowException)
            {
                //TODO log output here
                //$"Ignoring malformed(?) packet received from {remoteEndpoint}"
                //packetBytes.ToHexString()
            }
            catch (Exception ex)
            {
                //TODO log output here
                //$"Failed to receive packet from {remoteEndpoint}"
            }
            finally
            {
                Interlocked.Decrement(ref HandlerCount);
            }
        }

        /// <summary>
        /// Sends a packet
        /// </summary>
        /// <param name="responsePacket"></param>
        /// <param name="remoteEndpoint"></param>
        void SendResponsePacket(IRadiusPacket responsePacket, IPEndPoint remoteEndpoint)
        {
            var responseBytes = RadiusPacketParser.GetBytes(responsePacket);
            Server.Send(responseBytes, responseBytes.Length, remoteEndpoint);

            //TODO log output here
            //$"{responsePacket.Code} sent to {remoteEndpoint} Id={responsePacket.Identifier}"
        }

        /// <summary>
        /// Dump the packet attributes to the log
        /// </summary>
        /// <param name="packet"></param>
        void DumpPacket(IRadiusPacket packet)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Packet dump for {packet.Identifier}:");
            foreach (var attribute in packet.Attributes)
                if (attribute.Key == "User-Password")
                    sb.AppendLine($"{attribute.Key} length : {attribute.Value.First().ToString().Length}");
                else
                    attribute.Value.ForEach(o => sb.AppendLine($"{attribute.Key} : {o} [{o.GetType()}]"));

            //TODO log output here
            //sb.ToString()
        }
    }
}
