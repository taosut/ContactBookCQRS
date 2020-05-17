using ContactBookCQRS.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Events
{
    public class CategoryDeleteEvent : Event
    {
        public CategoryDeleteEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}
