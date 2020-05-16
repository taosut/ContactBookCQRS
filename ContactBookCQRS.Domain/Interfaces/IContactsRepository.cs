using ContactBookCQRS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactBookCQRS.Domain.Interfaces
{
    public interface IContactsRepository
    {
        Task CreateContact(Contact entity, CancellationToken cancellationToken = default);
        void UpdateContact(Contact entity);
        Contact GetByEmail(string email);
        IQueryable<Contact> GetContacts(Guid userId, Guid categoryId);
    }
}
