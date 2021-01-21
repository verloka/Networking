using SignalRChatShare.Models;
using System;

namespace SignalRChatWPFClient.Events
{
    public delegate void MessageEventHandler(object sender, MessageEventArgs e);

    public class MessageEventArgs : EventArgs
    {
        public Message Message { get; private set; }

        public MessageEventArgs(Message Message)
        {
            this.Message = Message;
        }
    }
}