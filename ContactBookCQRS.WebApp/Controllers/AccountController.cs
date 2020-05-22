using System.Threading.Tasks;
using ContactBookCQRS.Application.Interfaces;
using ContactBookCQRS.Domain.Events;
using ContactBookCQRS.Domain.Notifications;
using ContactBookCQRS.Infra.CrossCutting.Identity.Helpers;
using ContactBookCQRS.Infra.CrossCutting.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            INotificationHandler<DomainNotification> notificationHandler,
            IEventHandler mediator) : base(notificationHandler, mediator, userProvider)
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