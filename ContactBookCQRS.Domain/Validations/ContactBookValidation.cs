using ContactBookCQRS.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Validations
{
    public abstract class ContactBookValidation<T> : AbstractValidator<T> where T : ContactBookCommand
    {
        protected void ValidateHasUser()
        {
            RuleFor(c => c.UserId)
                .NotNull().WithMessage("Please ensure you have entered the User Id");
        }

        protected void ValidateUserId()
        {
            RuleFor(c => c.UserId)
                .NotEqual(String.Empty)
                .WithMessage("The User Id is required");
        }
    }
}
