using ContactBookCQRS.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Commands
{
    public abstract class ContactBookCommand : Command
    {
        public string UserId { get; protected set; }
    }
}
