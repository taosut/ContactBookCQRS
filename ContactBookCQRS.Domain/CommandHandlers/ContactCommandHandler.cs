using System;
using System.Threading;
using System.Threading.Tasks;
using ContactBookCQRS.Domain.Commands;
using ContactBookCQRS.Domain.Core.Bus;
using ContactBookCQRS.Domain.Core.Notifications;
using ContactBookCQRS.Domain.Interfaces;
using ContactBookCQRS.Domain.Models;
using MediatR;

namespace ContactBookCQRS.Domain.CommandHandlers
{
    public class ContactCommandHandler : CommandHandler,
        IRequestHandler<CreateNewContactCommand, bool>
    {
        private readonly IContactBookUnitOfWork _contactUnitOfWork;
        private readonly IMediatorHandler _bus;

        public ContactCommandHandler(
            IContactBookUnitOfWork uow,
            IMediatorHandler bus)
            : base(uow, bus)
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

            Contact contact = new Contact(new Guid(), request.Name, request.Email, request.BirthDate);
            _contactUnitOfWork.ContactsRepository.CreateContact(contact);
            _contactUnitOfWork.Commit();

            return Task.FromResult(true);
        }
    }
}
