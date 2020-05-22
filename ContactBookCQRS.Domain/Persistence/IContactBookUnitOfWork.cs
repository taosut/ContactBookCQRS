using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Persistence
{
    public interface IContactBookUnitOfWork : IUnitOfWork
    {
        IContactBooksRepository ContactBooksRepository { get; }
        IContactsRepository ContactsRepository { get; }
        ICategoriesRepository CategoriesRepository { get; }
    }
}
