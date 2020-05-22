using ContactBookCQRS.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookCQRS.Domain.CommandHandlers
{
    public interface ICommandHandler
    {
        Task SendCommand<T>(T command) where T : Command;
    }
}
