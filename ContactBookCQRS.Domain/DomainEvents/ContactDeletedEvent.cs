using ContactBookCQRS.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.DomainEvents
{
    public class ContactDeletedEvent : Event
    {
        public ContactDeletedEvent(Guid id)
        {
            AggregateId = id;
        }
    }
}
