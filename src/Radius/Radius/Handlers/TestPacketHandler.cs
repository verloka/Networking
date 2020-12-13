using Radius.Enums;
using Radius.Interfaces;
using System;

namespace Radius.Handlers
{
    public class TestPacketHandler : IPacketHandler
    {
        public string UserName { get; private set; }
        public string UserPassword { get; private set; }

        public TestPacketHandler() : this("user@example.com", "1234") { }
        public TestPacketHandler(string UserName, string UserPassword)
        {
            this.UserName = UserName;
            this.UserPassword = UserPassword;
        }

        public IRadiusPacket HandlePacket(IRadiusPacket packet)
        {
            if (packet.Code == PacketCode.AccountingRequest)
            {
                switch (packet.GetAttribute<AcctStatusType>("Acct-Status-Type"))
                {
                    case AcctStatusType.Start:
                    case AcctStatusType.Stop:
                    case AcctStatusType.InterimUpdate:
                        return packet.CreateResponsePacket(PacketCode.AccountingResponse);
                    default:
                        break;
                }
            }
            else if (packet.Code == PacketCode.AccessRequest)
            {
                if (packet.GetAttribute<string>("User-Name") == UserName && packet.GetAttribute<string>("User-Password") == UserPassword)
                {
                    var response = packet.CreateResponsePacket(PacketCode.AccessAccept);
                    response.AddAttribute("Acct-Interim-Interval", 60);
                    return response;
                }

                return packet.CreateResponsePacket(PacketCode.AccessReject);
            }

            throw new InvalidOperationException("Couldnt handle request?!");
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
        }
    }
}
