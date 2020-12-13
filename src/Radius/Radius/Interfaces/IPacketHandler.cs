using System;

namespace Radius.Interfaces
{
    public interface IPacketHandler : IDisposable
    {
        IRadiusPacket HandlePacket(IRadiusPacket packet);
    }
}
