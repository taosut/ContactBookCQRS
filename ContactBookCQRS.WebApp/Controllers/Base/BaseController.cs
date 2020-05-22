using System;
using System.Collections.Generic;
using System.Linq;
using ContactBookCQRS.Domain.Events;
using ContactBookCQRS.Domain.Notifications;
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
        private readonly IEventHandler _eventHandler;
        private readonly DomainNotificationHandler _notificationHandler;
        protected Guid UserId
        {
            get {
                return _userProvider.GetUserId();
            }
        }

        protected BaseController(
            INotificationHandler<DomainNotification> notifications,
            IEventHandler eventHandler,
            IUserProvider userProvider
            )
        {
            _eventHandler = eventHandler;
            _userProvider = userProvider;
            _notificationHandler = (DomainNotificationHandler)notifications;
        }

        protected IEnumerable<DomainNotification> Notifications => _notificationHandler.GetNotifications();

        protected bool IsValidOperation()
        {
            return (!_notificationHandler.HasNotifications());
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
                errors = _notificationHandler.GetNotifications().Select(n => n.Value)
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
            _eventHandler.RaiseEvent(new DomainNotification(code, message));
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
