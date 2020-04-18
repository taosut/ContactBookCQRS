using ContactBookCQRS.Domain.Interfaces;
using ContactBookCQRS.Domain.Models;
using ContactBookCQRS.Infra.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactBookCQRS.Infra.Persistence.Repository
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly ContactBookContext _dbContext;

        public ContactsRepository(ContactBookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateContact(Contact entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Contacts.AddAsync(entity);
        }
    }
}
