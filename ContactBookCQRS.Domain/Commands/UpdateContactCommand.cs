using ContactBookCQRS.Domain.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Commands
{
    public class UpdateContactCommand : ContactCommand
    {
        public UpdateContactCommand(Guid contactId, Guid userId, string name, string email, DateTime birthDate)
        {
            UserId = 
            Id = contactId;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateContactCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
