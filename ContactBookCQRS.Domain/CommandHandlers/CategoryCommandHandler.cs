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
    public class CategoryCommandHandler : CommandHandler,
        IRequestHandler<CreateNewCategoryCommand, bool>,
        IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IContactBookUnitOfWork _contactUnitOfWork;
        private readonly IMediatorHandler _bus;

        public CategoryCommandHandler(
            IContactBookUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _bus = bus;
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
                _bus.RaiseEvent(new CategoryCreatedEvent(category.Id, category.ContactBookId, category.Name));
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

            //Storing the creation event
            if (_contactUnitOfWork.Commit())
            {
                _bus.RaiseEvent(new CategoryUpdatedEvent(category.Id, category.ContactBookId, category.Name));
            }

            return Task.FromResult(true);
        }
    }
}
