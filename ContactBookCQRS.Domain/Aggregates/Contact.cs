using System;

namespace ContactBookCQRS.Domain.Aggregates
{
    public class Contact : Entity
    {
        public Contact(Guid id, Guid categoryId, 
            string name, string email, DateTime birthDate, string phoneNumber)
        {
            Id = id;
            CategoryId = categoryId;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
        }
        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string PhoneNumber { get; private set; }

        // Empty constructor for EF
        protected Contact() { }        
    }
}
