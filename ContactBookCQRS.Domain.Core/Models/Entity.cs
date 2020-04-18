using ContactBookCQRS.Domain.Core.Interfaces;
using System;

namespace ContactBookCQRS.Domain.Core.Models
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; protected set; }
    }
}
