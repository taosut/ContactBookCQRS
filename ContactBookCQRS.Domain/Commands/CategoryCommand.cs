using ContactBookCQRS.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Commands
{
    public abstract class CategoryCommand : Command
    {
        public Guid ContactBookId { get; protected set; }
        public string Name { get; protected set; }
    }
}
