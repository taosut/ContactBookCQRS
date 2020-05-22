using ContactBookCQRS.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactBookCQRS.Domain.Persistence
{
    public interface IContactBooksRepository
    {
        Task CreateContactBook(ContactBook entity, CancellationToken cancellationToken = default);
        Task<ContactBook> GetContactBookByUser(Guid userId, CancellationToken cancellationToken = default);
    }
}
