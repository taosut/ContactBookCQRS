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
        void DeleteContact(Guid userId, Guid entityId, CancellationToken cancellationToken = default);
        Contact GetByEmail(string email);
        IQueryable<Contact> GetContacts(Guid userId, Guid categoryId);
        void UpdateContact(Contact entity);

    }
}
