using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Interfaces
{
    public interface IUsersRepository
    {
        IUser GetById(string email);
    }
}
