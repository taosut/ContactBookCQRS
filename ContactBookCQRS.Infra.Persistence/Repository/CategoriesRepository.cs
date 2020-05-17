using ContactBookCQRS.Domain.Interfaces;
using ContactBookCQRS.Domain.Models;
using ContactBookCQRS.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;
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

        public async Task CreateCategory(Category entity, 
            CancellationToken cancellationToken = default)
        {
            await _dbContext.Categories.AddAsync(entity);
        }

        public void DeleteCategory(Guid userId,
            Guid entityId, CancellationToken cancellationToken = default)
        {
            var query = from ca in _dbContext.Categories
                        join cb in _dbContext.ContactBooks on ca.ContactBookId equals cb.Id
                        where cb.UserId == userId && ca.Id == entityId
                        select ca;

            var category = query.AsQueryable().FirstOrDefault();
            _dbContext.Categories.Remove(category);
        }

        public IQueryable<Category> GetCategories(Guid userId)
        {
            var query = from ca in _dbContext.Categories
                        join cb in _dbContext.ContactBooks on ca.ContactBookId equals cb.Id
                        where cb.UserId == userId
                        select ca;

            return _dbContext.Categories;
        }

        public void UpdateCategory(Category entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
