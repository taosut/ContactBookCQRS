using ContactBookCQRS.Domain.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Commands
{
    public class CreateNewCategoryCommand : CategoryCommand
    {
        public CreateNewCategoryCommand(Guid contactBookId, string name)
        {
            ContactBookId = contactBookId;
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateNewCategoryCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
