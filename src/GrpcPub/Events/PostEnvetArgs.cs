using System;

namespace GrpcPub.Events
{
    public delegate void PostEnvetHandler(object sender, PostEnvetArgs e);

    public class PostEnvetArgs : EventArgs
    {
        public Event Event { get; private set; }

        public PostEnvetArgs(Event Event)
        {
            this.Event = Event;
        }
    }
}
