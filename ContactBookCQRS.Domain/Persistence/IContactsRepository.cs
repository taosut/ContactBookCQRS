using ContactBookCQRS.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactBookCQRS.Domain.Persistence
{
    public interface IContactsRepository
    {
        Task CreateContact(Contact entity, CancellationToken cancellationToken = default);
        void DeleteContact(Guid userId, Guid entityId, CancellationToken cancellationToken = default);
        Contact GetByEmail(Guid userId, string email);
        IQueryable<Contact> GetContacts(Guid userId, Guid categoryId);
        void UpdateContact(Contact entity);

    }
}
