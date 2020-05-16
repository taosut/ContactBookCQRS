using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Interfaces
{
    public interface IUsersRepository
    {
        IUser GetById(Guid id);
        void Delete(IUser user);
    }
}
