using ContactBookCQRS.Domain.Core.Bus;
using ContactBookCQRS.Domain.Core.Commands;
using ContactBookCQRS.Domain.Core.Notifications;
using ContactBookCQRS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IContactBookUnitOfWork _uow;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(
            IContactBookUnitOfWork uow, 
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications()) 
                return false;

            if (_uow.Commit()) 
                return true;

            _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
            
            return false;
        }
    }
}
