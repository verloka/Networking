using Microsoft.AspNetCore.SignalR.Client;
using SignalRChatShare.Models;
using SignalRChatWPFClient.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRChatWPFClient.Core
{
    public class Chat : IAsyncDisposable
    {
        public event AlertEventHandler OnConnected;
        public event AlertEventHandler OnDisconnected;
        public event MessageEventHandler OnMessage;
        public event MessagesEventHandler OnMessages;
        public event GroupsEventHandler OnGroups;
        public event ErrorEventHandler OnError;

        readonly HubConnection Connection;

        public Chat(string URL, string Token)
        {
            Connection = new HubConnectionBuilder()
                .WithUrl(URL, (e) => { e.AccessTokenProvider = () => Task.FromResult(Token); })
                .Build();

            Connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await Connection.StartAsync();
            };
        }

        /// <summary>
        /// Init the connection
        /// </summary>
        /// <returns></returns>
        public void Init()
        {
            Connection.On<List<string>>("SendGroups", (e) => { OnGroups?.Invoke(this, new GroupsArgs(e)); });
            Connection.On<Message>("Send", (e) => { OnMessage?.Invoke(this, new MessageEventArgs(e)); });
            Connection.On<Message>("PM", (e) => { OnMessage?.Invoke(this, new MessageEventArgs(e)); });
            Connection.On<List<Message>>("SendBulk", (e) => { OnMessages?.Invoke(this, new MessagesEventArgs(e)); });
            Connection.On<LoginData>("Connected", (e) => { OnConnected?.Invoke(this, new AlertArgs(e)); });
            Connection.On<LoginData>("Disconnected", (e) => { OnDisconnected?.Invoke(this, new AlertArgs(e)); });
        }

        /// <summary>
        /// Start connection
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {
            try
            {
                await Connection.StartAsync();
            }
            catch (Exception e)
            {
                OnError?.Invoke(this, new ErrorArgs(e));
            }
        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="Text">Message text</param>
        /// <param name="Group">Group name</param>
        /// <returns></returns>
        public Task SendMessage(string Text, string Group) => Connection.InvokeAsync("Send", Text, Group);

        /// <summary>
        /// Send private message
        /// </summary>
        /// <param name="Text">Message text</param>
        /// <param name="Username">Username to</param>
        /// <param name="Group">Group name</param>
        /// <returns></returns>
        public Task SendMessage(string Text, string Username, string Group) => Connection.InvokeAsync("PM", Text, Username, Group);

        /// <summary>
        /// Open chat group
        /// </summary>
        /// <param name="Group">Group name</param>
        /// <returns></returns>
        public Task OpenGroup(string Group) => Connection.InvokeAsync("Enter", Group);

        public ValueTask DisposeAsync()
        {
            return ((IAsyncDisposable)Connection).DisposeAsync();
        }
    }
}
