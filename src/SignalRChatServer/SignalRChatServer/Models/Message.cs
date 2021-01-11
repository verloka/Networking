using System;

namespace SignalRChatServer.Models
{
    public class Message
    {
        public string ConnectionID { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
