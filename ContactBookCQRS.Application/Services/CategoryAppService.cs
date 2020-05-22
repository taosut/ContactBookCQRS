using System;
using AutoMapper;
using System.Collections.Generic;
using ContactBookCQRS.Application.Interfaces;
using ContactBookCQRS.Application.ViewModels;
using ContactBookCQRS.Domain.Commands;
using System.Linq;
using AutoMapper.QueryableExtensions;
using ContactBookCQRS.Application.EventSourcedNormalizers;
using ContactBookCQRS.Domain.CommandHandlers;
using ContactBookCQRS.Domain.Persistence;
using ContactBookCQRS.Domain.Aggregates;
using ContactBookCQRS.Application.EventSourceHelpers;

namespace ContactBookCQRS.Application.Services
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly IMapper _mapper;
        private readonly ICommandHandler _bus;
        private readonly IContactBookUnitOfWork _uow;
        private readonly IEventStoreRepository _eventStoreRepository;

        public CategoryAppService(
            IMapper mapper,
            IContactBookUnitOfWork uow, 
            ICommandHandler bus,
            IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _bus = bus;
            _uow = uow;
            _eventStoreRepository = eventStoreRepository;
        }

        public void CreateCategory(CategoryViewModel categoryViewModel)
        {
            var createCommand = _mapper.Map<CreateNewCategoryCommand>(categoryViewModel);
            _bus.SendCommand(createCommand);
        }

        public void DeleteCategory(Guid userId, Guid categoryId)
        {
            var deleteCommand = new DeleteCategoryCommand(userId, categoryId);
            _bus.SendCommand(deleteCommand);
        }

        public IEnumerable<CategoryViewModel> GetCategories(Guid userId)
        {
            return _uow.CategoriesRepository.GetCategories(userId)
                .ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider);
        }
 
        public void UpdateCategory(Guid categoryId, CategoryViewModel categoryViewModel)
        {
            categoryViewModel.Id = categoryId;
            var updateCommand = _mapper.Map<UpdateCategoryCommand>(categoryViewModel);
            _bus.SendCommand(updateCommand);
        }

        public IList<CategoryHistoryData> GetEventHistory(Guid id)
        {
            var storedEvents = _eventStoreRepository.GetByAggregateId(id);
            CategoryEventNormatizer normatizer = new CategoryEventNormatizer();
            IList<CategoryHistoryData> categoryHistoryData = normatizer.ToHistoryData(storedEvents);
            return categoryHistoryData;
        }
    }
}
