using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ContactBookCQRS.Application.ViewModels;
using ContactBookCQRS.Domain.Models;

namespace ContactBookCQRS.Application.AutoMapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Contact, ContactViewModel>();
        }
    }
}
