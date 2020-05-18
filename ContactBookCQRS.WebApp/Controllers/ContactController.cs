using ContactBookCQRS.Application.Interfaces;
using ContactBookCQRS.Application.ViewModels;
using ContactBookCQRS.Domain.Core.Bus;
using ContactBookCQRS.Domain.Core.Notifications;
using ContactBookCQRS.Infra.CrossCutting.Identity.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ontactBookCQRS.WebApp.Controllers.Base;
using System;

namespace ContactBookCQRS.WebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : BaseController
    {
        private readonly IUserProvider _userProvider;
        private readonly IContactAppService _contactAppService;

        public ContactController(
            IUserProvider userProvider,
            IContactAppService contactAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator, userProvider)
        {
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
            _contactAppService = contactAppService;
        }

        [Authorize(Policy = "CanWriteData")]
        [HttpDelete, Route("{contactId:guid}")]
        public IActionResult Delete([FromRoute]Guid contactId)
        {
            _contactAppService.DeleteContact(UserId, contactId);
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "CanWriteData")]
        public IActionResult Post([FromBody]ContactViewModel contactViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(contactViewModel);
            }

            _contactAppService.CreateContact(contactViewModel);

            return Response(contactViewModel);
        }

        [Authorize(Policy = "CanWriteData")]
        [HttpPut, Route("{contactId:guid}")]
        public IActionResult Put([FromRoute]Guid contactId, [FromBody]ContactViewModel contactViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(contactViewModel);
            }

            if (contactId != contactViewModel.Id)
            {
                 return BadRequest();
            }

            _contactAppService.UpdateContact(contactId, contactViewModel);
            return Ok();
        }
    }
}
