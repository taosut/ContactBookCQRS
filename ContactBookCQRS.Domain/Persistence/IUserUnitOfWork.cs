using ContactBookCQRS.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Persistence
{
    public interface IUserUnitOfWork : IUnitOfWork
    {
        IUsersRepository UsersRepository { get; }
    }
}
