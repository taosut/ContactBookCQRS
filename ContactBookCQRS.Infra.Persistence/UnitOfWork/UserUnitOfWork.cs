using ContactBookCQRS.Domain.Interfaces;
using ContactBookCQRS.Infra.CrossCutting.Identity.Models;
using ContactBookCQRS.Infra.Persistence.Context;
using ContactBookCQRS.Infra.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Infra.Persistence.UnitOfWork
{
    public class UserUnitOfWork : UnitOfWork<ApplicationDbContext>, IUserUnitOfWork
    {
        public UserUnitOfWork(
            ApplicationDbContext dbContext,
            IUsersRepository usersRepository
            ) : base(dbContext)
        {
            UsersRepository = usersRepository ?? throw new ArgumentNullException(nameof(UsersRepository));
        }

        public IUsersRepository UsersRepository { get; }
    }
}
