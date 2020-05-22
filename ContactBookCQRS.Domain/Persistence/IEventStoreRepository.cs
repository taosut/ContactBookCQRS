using ContactBookCQRS.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Persistence
{
    public interface IEventStoreRepository
    {
        IList<StoredEvent> GetByAggregateId(Guid aggregateId);
        void SaveEvent(StoredEvent @event);
    }
}
