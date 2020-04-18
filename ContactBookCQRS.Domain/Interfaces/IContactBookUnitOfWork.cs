using ContactBookCQRS.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Interfaces
{
    public interface IContactBookUnitOfWork : IUnitOfWork
    {
        IContactsRepository ContactsRepository { get; }
    }
}
