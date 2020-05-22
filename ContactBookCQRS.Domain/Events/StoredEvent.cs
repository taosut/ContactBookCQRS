using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Events
{
    public class StoredEvent : Event
    {
        public StoredEvent(Event @event, string metaData, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = @event.AggregateId;
            MessageType = @event.MessageType;
            MetaData = metaData;
            User = user;
        }

        // EF Constructor
        protected StoredEvent() { }

        public Guid Id { get; private set; }
        public string MetaData { get; private set; }
        public string User { get; private set; }
    }
}
