using ContactBookCQRS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactBookCQRS.Domain.Interfaces
{
    public interface IContactsRepository
    {
        Task CreateContact(Contact entity, CancellationToken cancellationToken = default);
        Contact GetByEmail(string email, CancellationToken cancellationToken = default);
    }
}
