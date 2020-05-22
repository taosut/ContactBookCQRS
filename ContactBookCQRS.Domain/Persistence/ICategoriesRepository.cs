using ContactBookCQRS.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactBookCQRS.Domain.Persistence
{
    public interface ICategoriesRepository
    {
        Task CreateCategory(Category entity, CancellationToken cancellationToken = default);
        void DeleteCategory(Guid userId, Guid entityId, CancellationToken cancellationToken = default);
        IQueryable<Category> GetCategories(Guid userId);
        void UpdateCategory(Category entity);
    }
}
