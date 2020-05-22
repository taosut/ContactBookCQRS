using ContactBookCQRS.Domain.Events;
using ContactBookCQRS.Domain.Identity;
using ContactBookCQRS.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ContactBookCQRS.Domain.Events
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IUser _user;

        public EventStoreService(IEventStoreRepository eventStoreRepository, IUser user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }

        public void StoreEvent<T>(T @event) where T : Event
        {
            var serializedData = JsonSerializer.Serialize(@event);

            var storedEvent = new StoredEvent(
                @event,
                serializedData,
                _user.Name);

            _eventStoreRepository.SaveEvent(storedEvent);
        }
    }
}
