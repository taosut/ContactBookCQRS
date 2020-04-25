using ContactBookCQRS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactBookCQRS.Domain.Interfaces
{
    public interface IContactBooksRepository
    {
        Task CreateContactBook(ContactBook entity, CancellationToken cancellationToken = default);
    }
}
