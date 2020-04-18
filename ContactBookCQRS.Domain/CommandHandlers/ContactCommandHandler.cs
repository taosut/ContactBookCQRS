﻿using ContactBookCQRS.Domain.Commands;
using ContactBookCQRS.Domain.Core.Bus;
using ContactBookCQRS.Domain.Interfaces;
using ContactBookCQRS.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactBookCQRS.Domain.CommandHandlers
{
    public class ContactCommandHandler : CommandHandler,
        IRequestHandler<CreateNewContactCommand, bool>
    {
        private readonly IContactBookUnitOfWork _contactUnitOfWork;
        private readonly IMediatorHandler _bus;

        public ContactCommandHandler(
            IContactBookUnitOfWork uow,
            IMediatorHandler bus)
            : base(uow, bus)
        {
            _bus = bus;
            _contactUnitOfWork = uow;
        }

        public Task<bool> Handle(CreateNewContactCommand request, CancellationToken cancellationToken)
        {
            if(!request.IsValid())
            {
                return Task.FromResult(false);
            }

            Contact contact = new Contact(new Guid(), request.Name, request.Email, request.BirthDate);
            _contactUnitOfWork.ContactsRepository.CreateContact(contact);
            _contactUnitOfWork.Commit();

            return Task.FromResult(true);
        }
    }
}
