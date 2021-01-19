using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRChatServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatServer.Services
{
    [Authorize]
    public class ChatHub : Hub
    {
        const string DEFAULT_GROUP_NAME = "default";

        List<string> PublicGroups = new List<string>
        {
            DEFAULT_GROUP_NAME,
            "cats",
            "dogs",
            "fish"
        };

        readonly StoreService storeService;

        public ChatHub(StoreService storeService)
        {
            this.storeService = storeService;
        }

        public async Task Enter(string group)
        {
            foreach (var g in PublicGroups)
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, g);

            await Groups.AddToGroupAsync(Context.ConnectionId, group);

            await Clients.Others.SendAsync("Connected", new LoginData { Date = DateTime.Now, Username = Context.User.Identity.Name });
            await Clients.Caller.SendAsync("SendBulk", storeService[group].Where(x => string.IsNullOrWhiteSpace(x.To) || x.To == Context.User.Identity.Name));
        }

        public async Task Send(string message, string group)
        {
            var msg = new Message
            {
                ConnectionID = Context.ConnectionId,
                Date = DateTime.Now,
                Text = message,
                Username = Context.User.Identity.Name,
                From = Context.User.Identity.Name
            };

            storeService[group].Add(msg);

            await Clients.Group(group).SendAsync("Send", msg);
        }

        public async Task PM(string message, string to, string group)
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

            storeService[group].Add(msg);

            await Clients.Users(to).SendAsync("PM", msg);
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, DEFAULT_GROUP_NAME);
            await Clients.Others.SendAsync("Connected", new LoginData { Date = DateTime.Now, Username = Context.User.Identity.Name });
            await Clients.Caller.SendAsync("SendBulk", storeService[DEFAULT_GROUP_NAME].Where(x => string.IsNullOrWhiteSpace(x.To) || x.To == Context.User.Identity.Name));
            await Clients.Caller.SendAsync("SendGroups", PublicGroups);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Others.SendAsync("Disconnected", new LoginData { Date = DateTime.Now, Username = Context.User.Identity.Name });
        }
    }
}
