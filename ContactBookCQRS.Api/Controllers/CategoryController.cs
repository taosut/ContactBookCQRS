using ContactBookCQRS.Api.Controllers.Base;
using ContactBookCQRS.Application.Interfaces;
using ContactBookCQRS.Application.ViewModels;
using ContactBookCQRS.Domain.Core.Bus;
using ContactBookCQRS.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactBookCQRS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ApiController
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoryController(
            ICategoryAppService categoryAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _categoryAppService = categoryAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Response(_categoryAppService.GetCategories());
        }

        [HttpPost]
        [Authorize(Policy = "CanWriteData")]
        public IActionResult Post([FromBody]CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(categoryViewModel);
            }

            _categoryAppService.CreateCategory(categoryViewModel);

            return Response(categoryViewModel);
        }
    }
}
