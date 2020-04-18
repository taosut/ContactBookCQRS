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
            IContactsRepository contactsRepository
            ) : base(dbContext)
        {
            ContactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
        }

        public IContactsRepository ContactsRepository { get; }
    }
}
