using ContactBookCQRS.Application.ViewModels;
using ContactBookCQRS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookCQRS.Application.Interfaces
{
    public interface IContactBookAppService
    {
        void CreateContactBook(Guid userId);
        Task<Guid> GetContactBookIdByUser(Guid userId);
    }
}
