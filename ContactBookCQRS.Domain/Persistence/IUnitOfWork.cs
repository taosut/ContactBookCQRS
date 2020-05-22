using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Persistence
{
    public interface IUnitOfWork
    {
        bool Commit();
    }
}
