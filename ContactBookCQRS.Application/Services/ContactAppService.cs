using System;
using AutoMapper;
using System.Collections.Generic;
using ContactBookCQRS.Application.Interfaces;
using ContactBookCQRS.Application.ViewModels;
using ContactBookCQRS.Domain.Commands;
using System.Linq;
using AutoMapper.QueryableExtensions;
using ContactBookCQRS.Domain.CommandHandlers;
using ContactBookCQRS.Domain.Persistence;
using ContactBookCQRS.Application.EventSourcedNormalizers;
using ContactBookCQRS.Application.EventSourceHelpers;

namespace ContactBookCQRS.Application.Services
{
    public class ContactAppService : IContactAppService
    {
        private readonly IMapper _mapper;
        private readonly ICommandHandler _bus;
        private readonly IContactBookUnitOfWork _uow;
        private readonly IEventStoreRepository _eventStoreRepository;

        public ContactAppService(
            IMapper mapper,
            IContactBookUnitOfWork uow,
            ICommandHandler bus,
            IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _bus = bus;
            _uow = uow;
            _eventStoreRepository = eventStoreRepository;
        }

        public void CreateContact(ContactViewModel contactViewModel)
        {
            var createCommand = _mapper.Map<CreateNewContactCommand>(contactViewModel);
            _bus.SendCommand(createCommand);
        }

        public void DeleteContact(Guid userId, Guid contactId)
        {
            var deleteCommand = new DeleteContactCommand(userId, contactId);
            _bus.SendCommand(deleteCommand);
        }

        public void UpdateContact(Guid contactId, ContactViewModel contactViewModel)
        {
            contactViewModel.Id = contactId;
            var updateCommand = _mapper.Map<UpdateContactCommand>(contactViewModel);
            _bus.SendCommand(updateCommand);
        }

        public IEnumerable<ContactViewModel> GetContacts(Guid userId, Guid categoryId)
        {
            return _uow.ContactsRepository.GetContacts(userId, categoryId)
                .ProjectTo<ContactViewModel>(_mapper.ConfigurationProvider);
        }

        public IList<ContactHistoryData> GetEventHistory(Guid id)
        {
            var storedEvents = _eventStoreRepository.GetByAggregateId(id);
            ContactEventNormatizer normatizer = new ContactEventNormatizer();
            IList<ContactHistoryData> contactHistoryData = normatizer.ToHistoryData(storedEvents);
            return contactHistoryData;
        }
    }
}
