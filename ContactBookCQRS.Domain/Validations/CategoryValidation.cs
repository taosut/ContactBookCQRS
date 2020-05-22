using ContactBookCQRS.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Validations
{
    public abstract class CategoryValidation<T> : AbstractValidator<T> where T : CategoryCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(5, 100).WithMessage("The Name must have between 5 and 100 characters");
        }

        protected void ValidateContactBookId()
        {
            RuleFor(c => c.ContactBookId)
                .NotEqual(Guid.Empty)
                .WithMessage("The ContactBook Id is required");
        }
    }
}
