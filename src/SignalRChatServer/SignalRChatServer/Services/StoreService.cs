using SignalRChatServer.Models;
using System.Collections.Generic;

namespace SignalRChatServer.Services
{
    public class StoreService
    {
        Dictionary<string, List<Message>> History { get; set; }

        public List<Message> this[string Key]
        {
            get
            {
                if (!History.ContainsKey(Key))
                    History.Add(Key, new List<Message>());

                return History[Key];
            }
        }

        public StoreService()
        {
            History = new Dictionary<string, List<Message>>();
        }
    }
}
