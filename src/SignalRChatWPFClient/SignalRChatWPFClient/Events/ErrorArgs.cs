using System;

namespace SignalRChatWPFClient.Events
{
    public delegate void ErrorEventHandler(object sender, ErrorArgs e);

    public class ErrorArgs
    {
        public Exception Exception { get; private set; }

        public ErrorArgs(Exception Exception)
        {
            this.Exception = Exception;
        }
    }
}