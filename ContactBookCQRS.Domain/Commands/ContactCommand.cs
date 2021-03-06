﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Commands
{
    public abstract class ContactCommand : Command
    {
        public Guid UserId { get; set; }
        public Guid Id { get; protected set; }
        public Guid CategoryId { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public DateTime BirthDate { get; protected set; }
        public string PhoneNumber { get; protected set; }
    }
}
