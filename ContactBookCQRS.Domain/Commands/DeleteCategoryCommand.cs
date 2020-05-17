using ContactBookCQRS.Domain.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Commands
{
    public class DeleteCategoryCommand : CategoryCommand
    {
        public Guid UserId { get; set; }
        public DeleteCategoryCommand(Guid userId, Guid categoryId)
        {
            Id = categoryId;
            UserId = userId;
            AggregateId = categoryId;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteCategoryCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
