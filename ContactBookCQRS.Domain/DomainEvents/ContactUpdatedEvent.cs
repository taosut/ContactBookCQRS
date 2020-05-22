using ContactBookCQRS.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.DomainEvents
{
    public class ContactUpdatedEvent : Event
    {
        public ContactUpdatedEvent(Guid id, string name, string email, DateTime birthDate, string phoneNumber)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            AggregateId = id;
        }
        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string PhoneNumber { get; private set; }

    }
}
