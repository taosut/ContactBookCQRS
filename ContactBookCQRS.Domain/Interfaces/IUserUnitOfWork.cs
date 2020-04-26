using ContactBookCQRS.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Interfaces
{
    public interface IUserUnitOfWork : IUnitOfWork
    {
        IUsersRepository UsersRepository { get; }
    }
}
