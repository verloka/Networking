using Radius.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace Radius.Handlers
{
    public class RadiusPacketHandlerRepository : IEnumerable<IPacketHandler>
    {
        private readonly Dictionary<IPAddress, (IPacketHandler packetHandler, string secret)> _packetHandlerAddresses = new Dictionary<IPAddress, (IPacketHandler, string)>();

        /// <summary>
        /// Add packet handler for remote endpoint
        /// </summary>
        /// <param name="remoteAddress"></param>
        /// <param name="packetHandler"></param>
        /// <param name="sharedSecret"></param>
        public void AddPacketHandler(IPAddress remoteAddress, IPacketHandler packetHandler, string sharedSecret)
        {
            _packetHandlerAddresses.Add(remoteAddress, (packetHandler, sharedSecret));
        }

        /// <summary>
        /// Add packet handler for multiple remote endpoints
        /// </summary>
        /// <param name="remoteAddresses"></param>
        /// <param name="packetHandler"></param>
        /// <param name="sharedSecret"></param>
        public void AddPacketHandler(List<IPAddress> remoteAddresses, IPacketHandler packetHandler, string sharedSecret)
        {
            foreach (var remoteAddress in remoteAddresses)
            {
                _packetHandlerAddresses.Add(remoteAddress, (packetHandler, sharedSecret));
            }
        }

        /// <summary>
        /// Try to find a packet handler for remote address
        /// </summary>
        /// <param name="remoteAddress"></param>
        /// <param name="packetHandler"></param>
        /// <returns></returns>
        public bool TryGetHandler(IPAddress remoteAddress, out (IPacketHandler packetHandler, string sharedSecret) handler)
        {
            if (_packetHandlerAddresses.TryGetValue(remoteAddress, out handler))
                return true;
            else if (_packetHandlerAddresses.TryGetValue(IPAddress.Any, out handler))
                return true;

            return false;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var item in _packetHandlerAddresses)
                yield return item.Value.packetHandler;
        }

        IEnumerator<IPacketHandler> IEnumerable<IPacketHandler>.GetEnumerator()
        {
            foreach (var item in _packetHandlerAddresses)
                yield return item.Value.packetHandler;
        }
    }
}
