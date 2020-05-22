﻿using ContactBookCQRS.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.DomainEvents
{
    public class CategoryDeletedEvent : Event
    {
        public CategoryDeletedEvent(Guid id)
        {
            AggregateId = id;
        }
    }
}
