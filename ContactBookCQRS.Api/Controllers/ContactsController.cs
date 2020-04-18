using ContactBookCQRS.Api.Controllers.Base;
using ContactBookCQRS.Application.Interfaces;
using ContactBookCQRS.Application.ViewModels;
using ContactBookCQRS.Domain.Core.Bus;
using ContactBookCQRS.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactBookCQRS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ApiController
    {
        private readonly IContactAppService _contactAppService;

        public ContactsController(
            IContactAppService contactAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _contactAppService = contactAppService;
        }

        [HttpPost]
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
    }
}
