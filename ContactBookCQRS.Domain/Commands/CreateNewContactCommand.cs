﻿using ContactBookCQRS.Domain.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Commands
{
    public class CreateNewContactCommand : ContactCommand
    {
        public CreateNewContactCommand(Guid categoryId, Guid userId, string name, string email, 
            DateTime birthDate, string phoneNumber)
        {
            UserId = userId;
            CategoryId = categoryId;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            PhoneNumber = PhoneNumber;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateNewContactCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
