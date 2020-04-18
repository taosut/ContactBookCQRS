using ContactBookCQRS.Domain.Core.Interfaces;
using ContactBookCQRS.Domain.Interfaces;
using ContactBookCQRS.Infra.Persistence.Context;
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

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }
    }
}

