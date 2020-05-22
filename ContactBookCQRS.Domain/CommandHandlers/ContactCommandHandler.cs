using System;
using System.Threading;
using System.Threading.Tasks;
using ContactBookCQRS.Domain.Aggregates;
using ContactBookCQRS.Domain.Commands;
using ContactBookCQRS.Domain.DomainEvents;
using ContactBookCQRS.Domain.Events;
using ContactBookCQRS.Domain.Notifications;
using ContactBookCQRS.Domain.Persistence;
using MediatR;

namespace ContactBookCQRS.Domain.CommandHandlers
{
    public class ContactCommandHandler : CommandHandler,
        IRequestHandler<CreateNewContactCommand, bool>,
        IRequestHandler<DeleteContactCommand, bool>,
        IRequestHandler<UpdateContactCommand, bool>        
    {
        private readonly IContactBookUnitOfWork _contactUnitOfWork;
        private readonly IEventHandler _eventHandler;

        public ContactCommandHandler(
            IContactBookUnitOfWork uow,
            IEventHandler eventHandler,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, eventHandler, notifications)
        {
            _eventHandler = eventHandler;
            _contactUnitOfWork = uow;
        }

        public Task<bool> Handle(CreateNewContactCommand request, CancellationToken cancellationToken)
        {
            if(!request.IsValid())
            {
                return Task.FromResult(false);
            }

            // Checking if contact e-mail is already taken
            if (null != _contactUnitOfWork.ContactsRepository.GetByEmail(request.Email))
            {
                _eventHandler.RaiseEvent(new DomainNotification(request.MessageType, "The contact e-mail has already been taken."));
                return Task.FromResult(false);
            }

            Contact contact = new Contact(
                new Guid(), 
                request.CategoryId, 
                request.Name, 
                request.Email, 
                request.BirthDate,
                request.PhoneNumber);

            _contactUnitOfWork.ContactsRepository.CreateContact(contact);
            
            //Storing the creation event
            if (_contactUnitOfWork.Commit())
            {
                _eventHandler.RaiseEvent(new ContactCreatedEvent(contact.Id, 
                    contact.CategoryId, 
                    contact.Name, 
                    contact.Email, 
                    contact.BirthDate, 
                    contact.PhoneNumber));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return Task.FromResult(false);
            }

            _contactUnitOfWork.ContactsRepository.DeleteContact(request.UserId, request.Id);

            //Storing the deletion event
            if (_contactUnitOfWork.Commit())
            {
                _eventHandler.RaiseEvent(new ContactDeletedEvent(request.Id));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return Task.FromResult(false);
            }

            var contact = new Contact(request.Id, 
                request.CategoryId, 
                request.Name, 
                request.Email, 
                request.BirthDate,
                request.PhoneNumber);

            var existingContact = _contactUnitOfWork.ContactsRepository.GetByEmail(request.Email);

            //Checking if the object is the same from db using this e-mail
            if (existingContact != null && 
                existingContact.Id != contact.Id)
            {                
                if (!existingContact.Equals(contact))
                {
                    _eventHandler.RaiseEvent(new DomainNotification(request.MessageType, "The contact e-mail has already been taken."));
                    return Task.FromResult(false);
                }
            }

            _contactUnitOfWork.ContactsRepository.UpdateContact(contact);

            //Storing the creation event
            if (_contactUnitOfWork.Commit())
            {
                _eventHandler.RaiseEvent(new ContactUpdatedEvent(contact.Id,
                    contact.Name,
                    contact.Email,
                    contact.BirthDate,
                    contact.PhoneNumber));
            }

            return Task.FromResult(true);
        }
    }
}
