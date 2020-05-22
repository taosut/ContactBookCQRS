using ContactBookCQRS.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Persistence
{
    public interface IUsersRepository
    {
        IUser GetById(Guid id);
        void Delete(IUser user);
    }
}
