using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRChatServer.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatServer.Services
{
    [Authorize]
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
                Username = Context.User.Identity.Name,
                From = Context.User.Identity.Name
            };

            storeService.GlobalChat.Add(msg);
            await Clients.All.SendAsync("Send", msg);
        }

        public async Task PM(string message, string to)
        {
            var msg = new Message
            {
                ConnectionID = Context.ConnectionId,
                Date = DateTime.Now,
                Text = message,
                Username = Context.User.Identity.Name,
                From = Context.User.Identity.Name,
                To = to
            };

            storeService.GlobalChat.Add(msg);

            await Clients.Users(to).SendAsync("PM", msg);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("Connected", new LoginData { Date = DateTime.Now, Username = Context.User.Identity.Name });
            await Clients.Caller.SendAsync("SendBulk", storeService.GlobalChat.Where(x => string.IsNullOrWhiteSpace(x.To) || x.To == Context.User.Identity.Name));
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Others.SendAsync("Disconnected", new LoginData { Date = DateTime.Now, Username = Context.User.Identity.Name });
        }
    }
}
