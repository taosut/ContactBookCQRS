using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ContactBookCQRS.Domain.Core.Bus;
using ContactBookCQRS.Domain.Core.Notifications;
using ContactBookCQRS.Infra.CrossCutting.Identity.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ontactBookCQRS.WebApp.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        private readonly IUserProvider _userProvider;
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;
        protected Guid UserId
        {
            get {
                return _userProvider.GetUserId();
            }
        }

        protected BaseController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            IUserProvider userProvider
            )
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
            _userProvider = userProvider;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected void NotifyModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            }
        }

        protected void NotifyError(string code, string message)
        {
            _mediator.RaiseEvent(new DomainNotification(code, message));
        }

        protected void AddIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                NotifyError(result.ToString(), error.Description);
            }
        }
    }
}
