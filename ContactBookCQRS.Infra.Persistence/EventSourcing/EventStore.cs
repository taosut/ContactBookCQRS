using ContactBookCQRS.Domain.Core.Events;
using ContactBookCQRS.Domain.Interfaces;
using ContactBookCQRS.Infra.Persistence.Repository.EventSourcing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ContactBookCQRS.Infra.Persistence.EventSourcing
{
    namespace Equinox.Infra.Data.EventSourcing
    {
        public class EventStore : IEventStore
        {
            private readonly IEventStoreRepository _eventStoreRepository;
            private readonly IUser _user;

            public EventStore(IEventStoreRepository eventStoreRepository, IUser user)
            {
                _eventStoreRepository = eventStoreRepository;
                _user = user;
            }

            public void Save<T>(T theEvent) where T : Event
            {
                var serializedData = JsonSerializer.Serialize(theEvent);

                var storedEvent = new StoredEvent(
                    theEvent,
                    serializedData,
                    _user.Name);

                _eventStoreRepository.Store(storedEvent);
            }
        }
    }
}
