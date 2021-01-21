using SignalRChatShare.Models;
using System;

namespace SignalRChatWPFClient.Events
{
    public delegate void AlertEventHandler(object sender, AlertArgs e);

    public class AlertArgs : EventArgs
    {
        public LoginData Data { get; private set; }

        public AlertArgs(LoginData Data)
        {
            this.Data = Data;
        }
    }
}
