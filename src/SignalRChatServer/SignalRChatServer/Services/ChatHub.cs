using Microsoft.AspNetCore.SignalR;
using SignalRChatServer.Models;
using System;
using System.Threading.Tasks;

namespace SignalRChatServer.Services
{
    public class ChatHub : Hub
    {
        readonly StoreService storeService;

        public ChatHub(StoreService storeService)
        {
            this.storeService = storeService;
        }

        public async Task Send(string message)
        {
            var msg = new Message
            {
                ConnectionID = Context.ConnectionId,
                Date = DateTime.Now,
                Text = message,
                Username = Context.ConnectionId
            };

            storeService.GlobalChat.Add(msg);

            await Clients.All.SendAsync("Send", msg);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("Connected", new LoginData { Date = DateTime.Now, Username = Context.ConnectionId });
            await Clients.Caller.SendAsync("SendBulk", storeService.GlobalChat);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Others.SendAsync("Disconnected", new LoginData { Date = DateTime.Now, Username = Context.ConnectionId });
        }
    }
}
