using ContactBookCQRS.Domain.Identity;
using ContactBookCQRS.Domain.Persistence;
using ContactBookCQRS.Infra.CrossCutting.Identity.Models;
using ContactBookCQRS.Infra.Persistence.Context;
using System;
using System.Linq;


namespace ContactBookCQRS.Infra.Persistence.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IdentityContext _dbContext;

        public UsersRepository(IdentityContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUser GetById(Guid Id)
        {
            return _dbContext.Users.FirstOrDefault(c => c.Id == Id);
        }

        public void Delete(IUser user)
        {            
            _dbContext.Users.Remove(user as User);
        }
    }
}
