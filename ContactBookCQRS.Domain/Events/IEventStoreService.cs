using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Events
{
    public interface IEventStoreService
    {
        void StoreEvent<T>(T @event) where T : Event;
    }
}
