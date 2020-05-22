using ContactBookCQRS.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.DomainEvents
{
    public class CategoryCreatedEvent : Event
    {
        public CategoryCreatedEvent(Guid id, string name)
        {
            AggregateId = id;
            Name = name;
        }
        public string Name { get; private set; }
    }
}
