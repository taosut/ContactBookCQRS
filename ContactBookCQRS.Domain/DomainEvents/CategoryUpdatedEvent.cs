using ContactBookCQRS.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.DomainEvents
{
    public class CategoryUpdatedEvent : Event
    {
        public CategoryUpdatedEvent(Guid id, Guid contactBookId, string name)
        {
            ContactBookId = contactBookId;
            Name = name;
            AggregateId = id;
        }
        public string Name { get; private set; }
        public Guid ContactBookId { get; private set; }

    }
}
