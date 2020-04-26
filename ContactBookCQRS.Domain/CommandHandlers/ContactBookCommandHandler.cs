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
        private readonly IUserUnitOfWork _userUnitOfWork;
        private readonly IMediatorHandler _bus;

        public ContactBookCommandHandler(
            IContactBookUnitOfWork contactUoW,
            IUserUnitOfWork userUnitOfWork,
            IMediatorHandler bus)
            : base(contactUoW, bus)
        {
            _bus = bus;
            _contactUnitOfWork = contactUoW;
            _userUnitOfWork = userUnitOfWork;
        }

        public Task<bool> Handle(CreateNewContactBookCommand request, CancellationToken cancellationToken)
        {
            if(!request.IsValid())
            {
                return Task.FromResult(false);
            }

            //Checking if user exists
            IUser user = _userUnitOfWork.UsersRepository.GetById(request.UserId);
            if(null != user)
            {
                ContactBook contactBook = new ContactBook(new Guid(), request.UserId);
                _contactUnitOfWork.ContactBooksRepository.CreateContactBook(contactBook);
                _contactUnitOfWork.Commit();

                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
