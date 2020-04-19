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
            CreateMap<ContactViewModel, CreateNewContactCommand>()
            .ConstructUsing(c => new CreateNewContactCommand(c.Name, c.Email, c.BirthDate));
        }
    }
}
