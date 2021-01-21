using System;

namespace SignalRChatWPFClient.Events
{
    public delegate void PMMessageEventHandler(object sender, PMMessageArgs e);

    public class PMMessageArgs : EventArgs
    {
        public string Username { get; private set; }

        public PMMessageArgs(string Username)
        {
            this.Username = Username;
        }
    }
}