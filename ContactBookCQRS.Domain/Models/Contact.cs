using ContactBookCQRS.Domain.Core.Models;
using System;

namespace ContactBookCQRS.Domain.Models
{
    public class Contact : Entity
    {
        public Contact(Guid id, Guid categoryId, string name, string email, DateTime birthDate)
        {
            Id = id;
            CategoryId = categoryId;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }
        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }

        // Empty constructor for EF
        protected Contact() { }        
    }
}
