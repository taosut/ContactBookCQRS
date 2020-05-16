using ContactBookCQRS.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Events
{
    public class CategoryCreatedEvent : Event
    {
        public CategoryCreatedEvent(Guid id, Guid contactBookId, string name)
        {
            Id = id;
            ContactBookId = contactBookId;
            Name = name;
            AggregateId = id;
        }
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public Guid ContactBookId { get; private set; }

    }
}
