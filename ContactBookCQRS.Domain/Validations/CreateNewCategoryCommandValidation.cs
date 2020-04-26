using ContactBookCQRS.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Validations
{
    public class CreateNewCategoryCommandValidation : CategoryValidation<CreateNewCategoryCommand>
    {
        public CreateNewCategoryCommandValidation()
        {
            ValidateContactBookId();
            ValidateName();
        }
    }
}
