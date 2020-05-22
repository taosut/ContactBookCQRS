using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Commands
{
    public abstract class ContactBookCommand : Command
    {
        public Guid UserId { get; protected set; }
    }
}
