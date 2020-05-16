using ContactBookCQRS.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookCQRS.Application.Interfaces
{
    public interface IContactAppService
    {
        void CreateContact(ContactViewModel contactViewModel);
        IEnumerable<ContactViewModel> GetContacts(Guid userId, Guid categoryId);
        void UpdateContact(Guid contactId, ContactViewModel contactViewModel);
    }
}
