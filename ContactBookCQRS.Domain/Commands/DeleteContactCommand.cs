using ContactBookCQRS.Domain.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Commands
{
    public class DeleteContactCommand : ContactCommand
    {
        public Guid UserId { get; set; }
        public DeleteContactCommand(Guid userId, Guid contactId)
        {
            Id = contactId;
            UserId = userId;
            AggregateId = contactId;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteContactCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
