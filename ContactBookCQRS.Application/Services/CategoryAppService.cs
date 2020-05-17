using System;
using AutoMapper;
using System.Collections.Generic;
using ContactBookCQRS.Application.Interfaces;
using ContactBookCQRS.Application.ViewModels;
using ContactBookCQRS.Domain.Commands;
using ContactBookCQRS.Domain.Core.Bus;
using ContactBookCQRS.Domain.Interfaces;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace ContactBookCQRS.Application.Services
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IContactBookUnitOfWork _uow;

        public CategoryAppService(
            IMapper mapper,
            IContactBookUnitOfWork uow, 
            IMediatorHandler bus)
        {
            _mapper = mapper;
            _bus = bus;
            _uow = uow;
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
    }
}
