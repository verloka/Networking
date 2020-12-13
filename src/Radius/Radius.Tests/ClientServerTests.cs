using Microsoft.VisualStudio.TestTools.UnitTesting;
using Radius.Core;
using Radius.Enums;
using System.Net;
using System.Threading.Tasks;

namespace Radius.Tests
{
    [TestClass]
    public class ClientServerTests
    {
        RadiusClient client;
        RadiusServer server;

        [TestCleanup]
        public void CleanupTests()
        {
            server?.Stop();
            server?.Dispose();

            client?.Dispose();
        }

        [TestMethod]
        public void AuthenticationServerLaunchTest()
        {
            server = RadiusFactory.CreateTestServer(1812, "secret", "test", "1234", RadiusServerType.Authentication);
            server.Start();
            Assert.AreEqual(true, server.Running);
        }

        [TestMethod]
        public void AccountingServerLaunchTest()
        {
            server = RadiusFactory.CreateTestServer(1813, "secret", "test", "1234", RadiusServerType.Accounting);
            server.Start();
            Assert.AreEqual(true, server.Running);
        }

        [TestMethod]
        public async Task AuthenticationSuccessTest()
        {
            server = RadiusFactory.CreateTestServer(1812, "secret", "test", "1234", RadiusServerType.Accounting);
            server.Start();

            client = RadiusFactory.CreateTestClient(1824);

            var packet = new RadiusPacket(PacketCode.AccessRequest, 0, "secret");
            packet.AddAttribute("User-Name", "test");
            packet.AddAttribute("User-Password", "1234");
            packet.AddAttribute("NAS-IP-Address", IPAddress.Parse("192.168.0.100"));
            packet.AddAttribute("NAS-Port", 3);

            var response = await client.SendPacketAsync(packet, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1812));
            Assert.AreEqual((uint)60, response.GetAttribute<uint>("Acct-Interim-Interval"));
        }

        [TestMethod]
        public async Task AuthenticationFail_1_Test()
        {
            server = RadiusFactory.CreateTestServer(1812, "secret", "testtest", "1234", RadiusServerType.Accounting);
            server.Start();

            client = RadiusFactory.CreateTestClient(1824);

            var packet = new RadiusPacket(PacketCode.AccessRequest, 0, "secret");
            packet.AddAttribute("User-Name", "test");
            packet.AddAttribute("User-Password", "1234");
            packet.AddAttribute("NAS-IP-Address", IPAddress.Parse("192.168.0.100"));
            packet.AddAttribute("NAS-Port", 3);

            var response = await client.SendPacketAsync(packet, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1812));
            Assert.AreEqual(PacketCode.AccessReject, response.Code);
        }

        [TestMethod]
        public async Task AuthenticationFail_2_Test()
        {
            server = RadiusFactory.CreateTestServer(1812, "secret", "test", "12345", RadiusServerType.Accounting);
            server.Start();

            client = RadiusFactory.CreateTestClient(1824);

            var packet = new RadiusPacket(PacketCode.AccessRequest, 0, "secret");
            packet.AddAttribute("User-Name", "test");
            packet.AddAttribute("User-Password", "1234");
            packet.AddAttribute("NAS-IP-Address", IPAddress.Parse("192.168.0.100"));
            packet.AddAttribute("NAS-Port", 3);

            var response = await client.SendPacketAsync(packet, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1812));
            Assert.AreEqual(PacketCode.AccessReject, response.Code);
        }

        [TestMethod]
        public async Task AuthenticationFail_3_Test()
        {
            server = RadiusFactory.CreateTestServer(1812, "secretsecret", "test", "1234", RadiusServerType.Accounting);
            server.Start();

            client = RadiusFactory.CreateTestClient(1824);

            var packet = new RadiusPacket(PacketCode.AccessRequest, 0, "secret");
            packet.AddAttribute("User-Name", "test");
            packet.AddAttribute("User-Password", "1234");
            packet.AddAttribute("NAS-IP-Address", IPAddress.Parse("192.168.0.100"));
            packet.AddAttribute("NAS-Port", 3);

            var response = await client.SendPacketAsync(packet, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1812));
            Assert.AreEqual(PacketCode.AccessReject, response.Code);
        }
    }
}
