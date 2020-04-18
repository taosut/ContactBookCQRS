using ContactBookCQRS.Application.Interfaces;
using ContactBookCQRS.Application.ViewModels;
using System;
using System.Collections.Generic;
using ContactBookCQRS.Domain.Commands;
using ContactBookCQRS.Domain.Core.Bus;

namespace ContactBookCQRS.Application.Services
{
    public class ContactAppService : IContactAppService
    {
        private readonly IMediatorHandler Bus;

        public ContactAppService(IMediatorHandler bus)
        {
            Bus = bus;
        }

        public ContactAppService()
        {
        }

        public void CreateContact(ContactViewModel contactViewModel)
        {
            var createCommand = new CreateNewContactCommand(
                contactViewModel.Name,
                contactViewModel.Email,
                contactViewModel.BirthDate);

            Bus.SendCommand(createCommand);
        }

        public IEnumerable<ContactViewModel> GetContacts()
        {
            throw new NotImplementedException();
        }
    }
}
