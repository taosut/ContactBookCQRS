using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Aggregates
{
    public class ContactBook : Entity, IAggregateRoot
    {
        public ContactBook(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }

        public Guid UserId { get; private set; }
        public ICollection<Category> Categories { get; set; }

        // Empty constructor for EF
        protected ContactBook() { }
    }
}
