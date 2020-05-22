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
    public class CategoryCommandHandler : CommandHandler,
        IRequestHandler<CreateNewCategoryCommand, bool>,
        IRequestHandler<DeleteCategoryCommand, bool>,
        IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IContactBookUnitOfWork _contactUnitOfWork;
        private readonly IEventHandler _eventHandler;

        public CategoryCommandHandler(
            IContactBookUnitOfWork uow,
            IEventHandler eventHandler,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, eventHandler, notifications)
        {
            _eventHandler = eventHandler;
            _contactUnitOfWork = uow;
        }

        public Task<bool> Handle(CreateNewCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return Task.FromResult(false);
            }

            Category category = new Category(new Guid(), request.ContactBookId, request.Name);
            _contactUnitOfWork.CategoriesRepository.CreateCategory(category);

            //Storing the creation event
            if (_contactUnitOfWork.Commit())
            {
                _eventHandler.RaiseEvent(new CategoryCreatedEvent(category.Id, category.Name));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return Task.FromResult(false);
            }

            _contactUnitOfWork.CategoriesRepository.DeleteCategory(request.UserId, request.Id);

            //Storing the deletion event
            if (_contactUnitOfWork.Commit())
            {
                _eventHandler.RaiseEvent(new CategoryDeletedEvent(request.Id));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return Task.FromResult(false);
            }

            Category category = new Category(request.Id, request.ContactBookId, request.Name);
            _contactUnitOfWork.CategoriesRepository.UpdateCategory(category);

            //Storing the update event
            if (_contactUnitOfWork.Commit())
            {
                _eventHandler.RaiseEvent(new CategoryUpdatedEvent(category.Id, category.ContactBookId, category.Name));
            }

            return Task.FromResult(true);
        }
    }
}
