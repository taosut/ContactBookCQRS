using ContactBookCQRS.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Events
{
    public class ContactDeleteEvent : Event
    {
        public ContactDeleteEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}
