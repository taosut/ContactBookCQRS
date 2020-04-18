using ContactBookCQRS.Domain.Core.Bus;
using ContactBookCQRS.Domain.Core.Commands;
using ContactBookCQRS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IContactBookUnitOfWork _uow;
        private readonly IMediatorHandler _bus;

        public CommandHandler(
            IContactBookUnitOfWork uow, 
            IMediatorHandler bus)
        {
            _uow = uow;
            _bus = bus;
        }
    }
}
