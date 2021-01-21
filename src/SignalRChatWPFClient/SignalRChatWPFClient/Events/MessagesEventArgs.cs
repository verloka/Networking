using SignalRChatShare.Models;
using System;
using System.Collections.Generic;

namespace SignalRChatWPFClient.Events
{
    public delegate void MessagesEventHandler(object sender, MessagesEventArgs e);

    public class MessagesEventArgs : EventArgs
    {
        public List<Message> Messages { get; private set; }

        public MessagesEventArgs(List<Message> Messages)
        {
            this.Messages = Messages;
        }
    }
}
