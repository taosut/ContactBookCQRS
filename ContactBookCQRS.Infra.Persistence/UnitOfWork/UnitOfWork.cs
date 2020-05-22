using ContactBookCQRS.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Infra.Persistence.UnitOfWork
{
    public abstract class UnitOfWork<TDB> : IUnitOfWork
            where TDB : DbContext
    {
        protected readonly TDB _dbContext;

        protected UnitOfWork(TDB dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public bool Commit()
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}

