using SignalRChatServer.Models;
using System.Collections.Generic;

namespace SignalRChatServer.Services
{
    public class StoreService
    {
        public List<Message> GlobalChat { get; private set; }

        public StoreService()
        {
            GlobalChat = new List<Message>();
        }
    }
}
