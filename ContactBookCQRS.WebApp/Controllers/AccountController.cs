using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ContactBookCQRS.Application.Interfaces;
using ContactBookCQRS.Application.Services;
using ContactBookCQRS.Application.ViewModels;
using ContactBookCQRS.Domain.Core.Bus;
using ContactBookCQRS.Domain.Core.Notifications;
using ContactBookCQRS.Infra.CrossCutting.Identity;
using ContactBookCQRS.Infra.CrossCutting.Identity.Helpers;
using ContactBookCQRS.Infra.CrossCutting.Identity.Models;
using ContactBookCQRS.WebApp.Configurations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ontactBookCQRS.WebApp.Controllers.Base;

namespace ContactBookCQRS.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountAppService _accountAppService;

        public AccountController(
            IUserProvider userProvider,
            IAccountAppService accountAppService,
            INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator) : base(notifications, mediator, userProvider)
        {
            _accountAppService = accountAppService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegistration userRegistration)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(userRegistration);
            }

            var result = await _accountAppService.Register(userRegistration);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    NotifyError(error.Code, error.Description);
                }

                return Response(userRegistration);
            }

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(userLogin);
            }

            var userViewModel = await _accountAppService.DoLogin(userLogin);

            if (userViewModel.LoginSucceeded)
            {
                return Response(userViewModel);
            }

            NotifyError("Login", userViewModel.LoginErrorMessage.ToString());
            return Response(userLogin);
        }

    }
}