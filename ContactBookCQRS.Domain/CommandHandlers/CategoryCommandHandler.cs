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
    public class CategoryCommandHandler : CommandHandler,
        IRequestHandler<CreateNewCategoryCommand, bool>
    {
        private readonly IContactBookUnitOfWork _contactUnitOfWork;
        private readonly IMediatorHandler _bus;

        public CategoryCommandHandler(
            IContactBookUnitOfWork uow,
            IMediatorHandler bus)
            : base(uow, bus)
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
            _contactUnitOfWork.Commit();

            return Task.FromResult(true);
        }
    }
}
