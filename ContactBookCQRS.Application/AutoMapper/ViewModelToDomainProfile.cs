using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ContactBookCQRS.Application.ViewModels;
using ContactBookCQRS.Domain.Commands;
using ContactBookCQRS.Domain.Models;

namespace ContactBookCQRS.Application.AutoMapper
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<CategoryViewModel, CreateNewCategoryCommand>()
            .ConstructUsing(c => new CreateNewCategoryCommand(c.ContactBookId, c.Name));

            CreateMap<ContactViewModel, CreateNewContactCommand>()
            .ConstructUsing(c => new CreateNewContactCommand(c.CategoryId, c.Name, c.Email, c.BirthDate));
        }
    }
}
