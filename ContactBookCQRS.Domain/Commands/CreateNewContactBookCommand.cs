﻿using ContactBookCQRS.Domain.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Commands
{
    public class CreateNewContactBookCommand : ContactBookCommand
    {
        public CreateNewContactBookCommand(Guid userId)
        {
            UserId = userId;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateNewContactBookCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
