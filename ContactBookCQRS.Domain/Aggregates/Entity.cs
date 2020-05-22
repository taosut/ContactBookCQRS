using System;

namespace ContactBookCQRS.Domain.Aggregates
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; protected set; }
    }
}
