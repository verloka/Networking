using Grpc.Core;
using GrpcPub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GrpcPub.Managers
{
    internal class SubscribersMap : Dictionary<string, Subscription>
    {
        public IEnumerable<IServerStreamWriter<Event>> this[EventType Type] { get => Values.Where(x => x.Type == Type).Select(x => x.StreamEvent); }

        public void SetStream(string Id, EventType Type, IServerStreamWriter<Event> StreamEvent)
        {
            if (ContainsKey(Id))
                base[Id].StreamEvent = StreamEvent;

            Add(Id, new Subscription(Type, StreamEvent));
        }
    }
}
