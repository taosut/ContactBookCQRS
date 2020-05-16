using ContactBookCQRS.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Validations
{
    public class UpdateContactCommandValidation : ContactValidation<UpdateContactCommand>
    {
        public UpdateContactCommandValidation()
        {
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}
