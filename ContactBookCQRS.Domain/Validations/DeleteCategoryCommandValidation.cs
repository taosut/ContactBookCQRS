using ContactBookCQRS.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Domain.Validations
{
    public class DeleteCategoryCommandValidation : CategoryValidation<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidation()
        {
            ValidateId();
        }
    }
}