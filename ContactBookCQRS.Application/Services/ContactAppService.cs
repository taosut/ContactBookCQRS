using System;
using AutoMapper;
using System.Collections.Generic;
using ContactBookCQRS.Application.Interfaces;
using ContactBookCQRS.Application.ViewModels;
using ContactBookCQRS.Domain.Commands;
using ContactBookCQRS.Domain.Core.Bus;
using ContactBookCQRS.Domain.Interfaces;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace ContactBookCQRS.Application.Services
{
    public class ContactAppService : IContactAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IContactBookUnitOfWork _uow;

        public ContactAppService(
            IMapper mapper,
            IContactBookUnitOfWork uow, 
            IMediatorHandler bus)
        {
            _mapper = mapper;
            _bus = bus;
            _uow = uow;
        }

        public void CreateContact(ContactViewModel contactViewModel)
        {
            var createCommand = _mapper.Map<CreateNewContactCommand>(contactViewModel);
            _bus.SendCommand(createCommand);
        }

        public IEnumerable<ContactViewModel> GetContacts()
        {
            return _uow.ContactsRepository.GetContacts()
                .ProjectTo<ContactViewModel>(_mapper.ConfigurationProvider);
        }
    }
}
