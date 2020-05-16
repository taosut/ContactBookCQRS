using ContactBookCQRS.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Infra.Persistence.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}
