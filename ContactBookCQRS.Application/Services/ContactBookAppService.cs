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
using ContactBookCQRS.Domain.Models;
using System.Threading.Tasks;

namespace ContactBookCQRS.Application.Services
{
    public class ContactBookAppService : IContactBookAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IContactBookUnitOfWork _uow;

        public ContactBookAppService(
            IMapper mapper,
            IContactBookUnitOfWork uow, 
            IMediatorHandler bus)
        {
            _mapper = mapper;
            _bus = bus;
            _uow = uow;
        }

        public void CreateContactBook(Guid userId)
        {
            var createCommand = new CreateNewContactBookCommand(userId);
            _bus.SendCommand(createCommand);
        }

        public async Task<Guid> GetContactBookIdByUser(Guid userId)
        {
            ContactBook contactBook = await _uow.ContactBooksRepository.GetContactBookByUser(userId);

            if (null != contactBook)
                return contactBook.Id;

            return Guid.Empty;
        }
    }
}
