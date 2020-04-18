using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Core.Interfaces
{
    /// <summary>
    /// Defines an entity through an Unique-Identifier
    /// </summary>
    public interface IEntity
    {
        Guid Id { get; }
    }
}
