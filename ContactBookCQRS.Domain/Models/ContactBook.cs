using ContactBookCQRS.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ContactBookCQRS.Domain.Models
{
    public class ContactBook : Entity
    {
        public ContactBook(Guid id, string userId)
        {
            Id = id;
            UserId = userId;
        }

        public string UserId { get; private set; }

        // Empty constructor for EF
        protected ContactBook() { }

    }
}
