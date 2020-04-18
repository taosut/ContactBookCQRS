using ContactBookCQRS.Domain.Core.Models;
using System;

namespace ContactBookCQRS.Domain.Models
{
    public class Contact : Entity
    {
        public Contact(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        // Empty constructor for EF
        protected Contact() { }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
    }
}
