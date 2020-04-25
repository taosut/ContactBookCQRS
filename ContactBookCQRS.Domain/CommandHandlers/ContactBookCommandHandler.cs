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
    public class ContactBookCommandHandler : CommandHandler,
        IRequestHandler<CreateNewContactBookCommand, bool>
    {
        private readonly IContactBookUnitOfWork _contactUnitOfWork;
        private readonly IMediatorHandler _bus;

        public ContactBookCommandHandler(
            IContactBookUnitOfWork uow,
            IMediatorHandler bus)
            : base(uow, bus)
        {
            _bus = bus;
            _contactUnitOfWork = uow;
        }

        public Task<bool> Handle(CreateNewContactBookCommand request, CancellationToken cancellationToken)
        {
            if(!request.IsValid())
            {
                return Task.FromResult(false);
            }

            ContactBook contactBook = new ContactBook(new Guid(), request.UserId);
            _contactUnitOfWork.ContactBooksRepository.CreateContactBook(contactBook);
            _contactUnitOfWork.Commit();

            return Task.FromResult(true);
        }
    }
}
