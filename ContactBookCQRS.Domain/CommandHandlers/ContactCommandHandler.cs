using System;
using System.Threading;
using System.Threading.Tasks;
using ContactBookCQRS.Domain.Commands;
using ContactBookCQRS.Domain.Core.Bus;
using ContactBookCQRS.Domain.Core.Notifications;
using ContactBookCQRS.Domain.Events;
using ContactBookCQRS.Domain.Interfaces;
using ContactBookCQRS.Domain.Models;
using MediatR;

namespace ContactBookCQRS.Domain.CommandHandlers
{
    public class ContactCommandHandler : CommandHandler,
        IRequestHandler<CreateNewContactCommand, bool>,
        IRequestHandler<DeleteContactCommand, bool>,
        IRequestHandler<UpdateContactCommand, bool>        
    {
        private readonly IContactBookUnitOfWork _contactUnitOfWork;
        private readonly IMediatorHandler _bus;

        public ContactCommandHandler(
            IContactBookUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _bus = bus;
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
                _bus.RaiseEvent(new DomainNotification(request.MessageType, "The customer e-mail has already been taken."));
                return Task.FromResult(false);
            }

            Contact contact = new Contact(
                new Guid(), 
                request.CategoryId, 
                request.Name, 
                request.Email, 
                request.BirthDate);

            _contactUnitOfWork.ContactsRepository.CreateContact(contact);
            _contactUnitOfWork.Commit();

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
                _bus.RaiseEvent(new ContactDeleteEvent(request.Id));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return Task.FromResult(false);
            }

            var contact = new Contact(request.Id, request.CategoryId, request.Name, request.Email, request.BirthDate);
            var existingContact = _contactUnitOfWork.ContactsRepository.GetByEmail(request.Email);

            //Checking if the object is the same from db using this e-mail
            if (existingContact != null && 
                existingContact.Id != contact.Id)
            {                
                if (!existingContact.Equals(contact))
                {
                    _bus.RaiseEvent(new DomainNotification(request.MessageType, "The customer e-mail has already been taken."));
                    return Task.FromResult(false);
                }
            }

            _contactUnitOfWork.ContactsRepository.UpdateContact(contact);
            _contactUnitOfWork.Commit();

            return Task.FromResult(true);
        }
    }
}
