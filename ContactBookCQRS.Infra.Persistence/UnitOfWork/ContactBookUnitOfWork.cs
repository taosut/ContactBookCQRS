using ContactBookCQRS.Domain.Interfaces;
using ContactBookCQRS.Infra.Persistence.Context;
using ContactBookCQRS.Infra.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Infra.Persistence.UnitOfWork
{
    public class ContactBookUnitOfWork : UnitOfWork<ContactBookContext>, IContactBookUnitOfWork
    {
        public ContactBookUnitOfWork(
            ContactBookContext dbContext,
            IContactBooksRepository contactsBooksRepository,
            ICategoriesRepository categoriesRepository,
            IContactsRepository contactsRepository
            ) : base(dbContext)
        {
            ContactBooksRepository = contactsBooksRepository ?? throw new ArgumentNullException(nameof(contactsBooksRepository));
            CategoriesRepository = categoriesRepository ?? throw new ArgumentNullException(nameof(categoriesRepository));
            ContactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
        }

        public IContactBooksRepository ContactBooksRepository { get; }
        public ICategoriesRepository CategoriesRepository { get; }
        public IContactsRepository ContactsRepository { get; }        
    }
}
