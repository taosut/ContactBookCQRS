using System;
using AutoMapper;
using System.Collections.Generic;
using ContactBookCQRS.Application.Interfaces;
using ContactBookCQRS.Application.ViewModels;
using ContactBookCQRS.Domain.Commands;
using ContactBookCQRS.Domain.Core.Bus;
using ContactBookCQRS.Domain.Interfaces;
using System.Linq;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;
using ContactBookCQRS.Infra.CrossCutting.Identity.Services;
using ContactBookCQRS.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Identity;
using ContactBookCQRS.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ContactBookCQRS.Application.Services
{

    public class AccountAppService : IAccountAppService
    {
        private readonly IContactBookAppService _contactBookAppService;
        private readonly IMediatorHandler _bus;
        private readonly IContactBookUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;


        public AccountAppService(
            IContactBookAppService contactBookAppService,
            IContactBookUnitOfWork uow, 
            IMediatorHandler bus,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IJwtService jwtService)
        {
            _contactBookAppService = contactBookAppService;
            _bus = bus;
            _uow = uow;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;

        }

        public async Task<UserViewModel> DoLogin(UserLogin userLogin)
        {
            UserViewModel userViewModel = new UserViewModel();
            var result = await _signInManager.
                PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

            userViewModel.LoginSucceeded = result.Succeeded;

            if(!userViewModel.LoginSucceeded)
            {
                userViewModel.LoginErrorMessage = "Usename or password invalid";
                return userViewModel;
            }

            var token = await _jwtService.GenerateJwt(userLogin.Email);
            var user = await _userManager.FindByEmailAsync(userLogin.Email);
            var contactBookId = await _contactBookAppService.GetContactBookIdByUser(user.Id);

            userViewModel.ContactBookId = contactBookId;
            userViewModel.Email = user.Email;
            userViewModel.Token = token;
            
            return userViewModel;
        }

        public async Task<IdentityResult> Register(UserRegistration userRegistration)
        {
            var user = new User(_httpContextAccessor)
            {
                UserName = userRegistration.Email,
                Email = userRegistration.Email
            };

            var result = await _userManager.CreateAsync(user, userRegistration.Password);

            if(result.Succeeded)
            {
                // User claim for write and delete
                await _userManager.AddClaimAsync(user, new Claim("CanWriteData", "Write"));
                await _userManager.AddClaimAsync(user, new Claim("CanDeleteData", "Delete"));

                // Creates a ContactBook for the user
                _contactBookAppService.CreateContactBook(user.Id);
            }

            return result;
        }

    }
}
