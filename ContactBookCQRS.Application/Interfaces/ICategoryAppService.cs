using ContactBookCQRS.Application.EventSourcedNormalizers;
using ContactBookCQRS.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookCQRS.Application.Interfaces
{
    public interface ICategoryAppService
    {
        void CreateCategory(CategoryViewModel categoryViewModel);
        IEnumerable<CategoryViewModel> GetCategories(Guid userId);
        void UpdateCategory(Guid categoryId, CategoryViewModel categoryViewModel);
        void DeleteCategory(Guid userId, Guid categoryId);
        IList<CategoryHistoryData> GetEventHistory(Guid id);
    }
}
