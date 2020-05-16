using ContactBookCQRS.Domain.Core.Events;
using ContactBookCQRS.Domain.Core.Interfaces;
using ContactBookCQRS.Infra.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactBookCQRS.Infra.Persistence.Repository.EventSourcing
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventStoreContext _context;

        public EventStoreRepository(EventStoreContext context)
        {
            _context = context;
        }

        public IList<StoredEvent> All(Guid aggregateId)
        {
            return _context.StoredEvents.Where(e => e.AggregateId == aggregateId).ToList();
        }

        public void Store(StoredEvent theEvent)
        {
            _context.StoredEvents.Add(theEvent);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
