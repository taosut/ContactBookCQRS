using System;
using System.Threading;
using System.Threading.Tasks;
using ContactBookCQRS.Domain.Aggregates;
using ContactBookCQRS.Domain.Commands;
using ContactBookCQRS.Domain.Events;
using ContactBookCQRS.Domain.Identity;
using ContactBookCQRS.Domain.Notifications;
using ContactBookCQRS.Domain.Persistence;
using MediatR;

namespace ContactBookCQRS.Domain.CommandHandlers
{
    public class ContactBookCommandHandler : CommandHandler,
        IRequestHandler<CreateNewContactBookCommand, bool>
    {
        private readonly IContactBookUnitOfWork _contactUnitOfWork;
        private readonly IUserUnitOfWork _userUnitOfWork;
        private readonly IEventHandler _eventHandler;

        public ContactBookCommandHandler(
            IContactBookUnitOfWork contactUoW,
            IUserUnitOfWork userUnitOfWork,
            IEventHandler eventHandler,
            INotificationHandler<DomainNotification> notifications)
            : base(contactUoW, eventHandler, notifications)
        {
            _eventHandler = eventHandler;
            _contactUnitOfWork = contactUoW;
            _userUnitOfWork = userUnitOfWork;
        }

        public Task<bool> Handle(CreateNewContactBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!request.IsValid())
                {
                    return Task.FromResult(false);
                }

                ContactBook contactBook = new ContactBook(new Guid(), request.UserId);
                _contactUnitOfWork.ContactBooksRepository.CreateContactBook(contactBook);
                _contactUnitOfWork.Commit();
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                DeleteUser(request.UserId);
                throw;
            }              
        }

        private void DeleteUser(Guid userId)
        {
            //Checking if user exists
            IUser user = _userUnitOfWork.UsersRepository.GetById(userId);

            if (null != user)
            {
                _userUnitOfWork.UsersRepository.Delete(user);
                _userUnitOfWork.Commit();
            }
        }
    }
}
