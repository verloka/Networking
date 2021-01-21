using System;

namespace SignalRChatShare.Models
{
    public class Message
    {
        public string From { get; set; }
        public string To { get; set; }
        public string ConnectionID { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
