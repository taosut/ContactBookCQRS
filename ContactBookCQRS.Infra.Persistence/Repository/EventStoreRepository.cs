using ContactBookCQRS.Domain.Events;
using ContactBookCQRS.Domain.Persistence;
using ContactBookCQRS.Infra.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactBookCQRS.Infra.Persistence.Repository
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly StoredEventContext _context;

        public EventStoreRepository(StoredEventContext context)
        {
            _context = context;
        }

        public IList<StoredEvent> GetByAggregateId(Guid aggregateId)
        {
            return _context.StoredEvents.Where(e => e.AggregateId == aggregateId).ToList();
        }

        public void SaveEvent(StoredEvent storedEvent)
        {
            _context.StoredEvents.Add(storedEvent);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
