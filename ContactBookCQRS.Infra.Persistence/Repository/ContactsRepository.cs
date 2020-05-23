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
    public class ContactsRepository : IContactsRepository
    {
        private readonly ContactBookContext _dbContext;

        public ContactsRepository(ContactBookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateContact(Contact entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Contacts.AddAsync(entity, cancellationToken);
        }

        public void DeleteContact(Guid userId,
            Guid contactId, CancellationToken cancellationToken = default)
        {
            var query = from ct in _dbContext.Contacts
                        join ca in _dbContext.Categories on ct.CategoryId equals ca.Id
                        join cb in _dbContext.ContactBooks on ca.ContactBookId equals cb.Id
                        where cb.UserId == userId && ct.Id == contactId
                        select ct;

            var contact = query.AsQueryable().FirstOrDefault();
            _dbContext.Contacts.Remove(contact);
        }

        public Contact GetByEmail(Guid userId, string email)
        {
            var query = from ct in _dbContext.Contacts
                        join ca in _dbContext.Categories on ct.CategoryId equals ca.Id
                        join cb in _dbContext.ContactBooks on ca.ContactBookId equals cb.Id
                        where cb.UserId == userId && ct.Email == email
                        select ct;

            return query.AsNoTracking().FirstOrDefault();
        }

        public IQueryable<Contact> GetContacts(Guid userId, Guid categoryId)
        {
            var query = from ct in _dbContext.Contacts
                        join ca in _dbContext.Categories on ct.CategoryId equals ca.Id
                        join cb in _dbContext.ContactBooks on ca.ContactBookId equals cb.Id
                        where cb.UserId == userId && ca.Id == categoryId
                        select ct;

            return query.AsQueryable();
        }

        public void UpdateContact(Contact entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;            
        }
    }
}
