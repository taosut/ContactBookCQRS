using ContactBookCQRS.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Validations
{
    public class CreateNewContactBookCommandValidation : ContactBookValidation<CreateNewContactBookCommand>
    {
        public CreateNewContactBookCommandValidation()
        {
            ValidateHasUser();
        }
    }
}
