using ContactBookCQRS.Domain.Identity;
using ContactBookCQRS.Domain.Persistence;
using ContactBookCQRS.Infra.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Infra.Persistence.UnitOfWork
{
    public class UserUnitOfWork : UnitOfWork<IdentityContext>, IUserUnitOfWork
    {
        public UserUnitOfWork(
            IdentityContext dbContext,
            IUsersRepository usersRepository
            ) : base(dbContext)
        {
            UsersRepository = usersRepository ?? throw new ArgumentNullException(nameof(UsersRepository));
        }

        public IUsersRepository UsersRepository { get; }
    }
}
