using ContactBookCQRS.Domain.Aggregates;
using ContactBookCQRS.Domain.Persistence;
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
    public class ContactBooksRepository : IContactBooksRepository
    {
        private readonly ContactBookContext _dbContext;

        public ContactBooksRepository(ContactBookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateContactBook(ContactBook entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.ContactBooks.AddAsync(entity);
        }

        public async Task<ContactBook> GetContactBookByUser(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.ContactBooks
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
