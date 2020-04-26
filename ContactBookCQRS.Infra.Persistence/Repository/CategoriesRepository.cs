using ContactBookCQRS.Domain.Interfaces;
using ContactBookCQRS.Domain.Models;
using ContactBookCQRS.Infra.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactBookCQRS.Infra.Persistence.Repository
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ContactBookContext _dbContext;

        public CategoriesRepository(ContactBookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateCategory(Category entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Categories.AddAsync(entity);
        }

        public IQueryable<Category> GetCategories()
        {
            return _dbContext.Categories;
        }
    }
}
