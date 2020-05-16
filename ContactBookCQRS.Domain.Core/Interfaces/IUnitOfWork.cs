using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Core.Interfaces
{
    public interface IUnitOfWork
    {
        bool Commit();
    }
}
