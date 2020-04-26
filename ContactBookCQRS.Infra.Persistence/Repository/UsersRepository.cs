using ContactBookCQRS.Domain.Interfaces;
using ContactBookCQRS.Domain.Models;
using ContactBookCQRS.Infra.CrossCutting.Identity.Models;
using ContactBookCQRS.Infra.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactBookCQRS.Infra.Persistence.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UsersRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUser GetById(string Id)
        {
            return _dbContext.Users.FirstOrDefault(c => c.Id == Id);
        }
    }
}
