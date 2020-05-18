using ContactBookCQRS.Application.ViewModels;
using ContactBookCQRS.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookCQRS.Application.Interfaces
{
    public interface IAccountAppService
    {
        Task<UserViewModel> DoLogin(UserLogin userLogin);
        Task<IdentityResult> Register(UserRegistration userRegistration);
    }
}
