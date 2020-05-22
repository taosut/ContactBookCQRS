using ContactBookCQRS.Domain.Events;
using ContactBookCQRS.Domain.Notifications;
using ContactBookCQRS.Domain.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IContactBookUnitOfWork _uow;
        private readonly IEventHandler _eventHandler;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(
            IContactBookUnitOfWork uow, 
            IEventHandler eventHandler,
            INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _eventHandler = eventHandler;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications()) 
                return false;

            if (_uow.Commit()) 
                return true;

            _eventHandler.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
            
            return false;
        }
    }
}
