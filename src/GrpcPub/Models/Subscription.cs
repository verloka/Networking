using Grpc.Core;

namespace GrpcPub.Models
{
    internal class Subscription
    {
        public EventType Type { get; private set; }
        public IServerStreamWriter<Event> StreamEvent { get; set; }

        private Subscription() { }

        public Subscription(EventType Type, IServerStreamWriter<Event> StreamEvent)
        {
            this.Type = Type;
            this.StreamEvent = StreamEvent;
        }
    }
}
