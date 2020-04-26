using ContactBookCQRS.Domain.Core.Interfaces;
using ContactBookCQRS.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ContactBookCQRS.Domain.Models
{
    public class ContactBook : Entity, IAggregateRoot
    {
        public ContactBook(Guid id, string userId)
        {
            Id = id;
            UserId = userId;
        }

        public string UserId { get; private set; }
        public ICollection<Category> Categories { get; set; }

        // Empty constructor for EF
        protected ContactBook() { }
    }
}
