using Radius.Core;
using Radius.Enums;
using Radius.Handlers;
using System;
using System.IO;
using System.Net;

namespace Radius
{
    public static class RadiusFactory
    {
        /// <summary>
        /// Get radius server for test
        /// </summary>
        /// <param name="Port">Server port</param>
        /// <param name="Secret">Packet secret</param>
        /// <param name="UserName">User name</param>
        /// <param name="UserPassword">User password</param>
        /// <param name="ServerType">Server type</param>
        /// <returns></returns>
        public static RadiusServer CreateTestServer(int Port, string Secret, string UserName, string UserPassword, RadiusServerType ServerType)
        {
            var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "/Content/radius.dictionary";
            var radiusPacketParser = new RadiusPacketParser(new RadiusDictionary(path));
            var packetHandler = new TestPacketHandler(UserName, UserPassword);
            var repository = new RadiusPacketHandlerRepository();
            repository.AddPacketHandler(IPAddress.Any, packetHandler, Secret);
            return new RadiusServer(new IPEndPoint(IPAddress.Any, Port), radiusPacketParser, ServerType, repository);
        }

        public static RadiusClient CreateTestClient(int Port)
        {
            var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "/Content/radius.dictionary";
            var radiusPacketParser = new RadiusPacketParser(new RadiusDictionary(path));
            var client = new RadiusClient(new IPEndPoint(IPAddress.Any, Port), radiusPacketParser);

            return client;
        }
    }
}
