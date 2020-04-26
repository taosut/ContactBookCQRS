using ContactBookCQRS.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ContactBookCQRS.Domain.Models
{
    public class Category : Entity
    {
        public Category(Guid id, Guid contactBookId, string name)
        {
            Id = id;
            ContactBookId = contactBookId;
            Name = name;
        }

        public string Name { get; private set; }
        public ICollection<Contact> Contacts { get; set; }
        public Guid ContactBookId { get; private set; }

        // Empty constructor for EF
        protected Category() { }
    }
}
