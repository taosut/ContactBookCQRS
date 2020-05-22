using System;
using AutoMapper;
using ContactBookCQRS.Application.Interfaces;
using ContactBookCQRS.Domain.Commands;
using System.Linq;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;
using ContactBookCQRS.Domain.CommandHandlers;
using ContactBookCQRS.Domain.Aggregates;
using ContactBookCQRS.Domain.Persistence;

namespace ContactBookCQRS.Application.Services
{
    public class ContactBookAppService : IContactBookAppService
    {
        private readonly IMapper _mapper;
        private readonly ICommandHandler _bus;
        private readonly IContactBookUnitOfWork _uow;

        public ContactBookAppService(
            IMapper mapper,
            IContactBookUnitOfWork uow,
            ICommandHandler bus)
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
