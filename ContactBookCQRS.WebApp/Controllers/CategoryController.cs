using ContactBookCQRS.Application.Interfaces;
using ContactBookCQRS.Application.ViewModels;
using ContactBookCQRS.Domain.Events;
using ContactBookCQRS.Domain.Notifications;
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
    public class CategoryController : BaseController
    {
        private readonly ICategoryAppService _categoryAppService;
        private readonly IContactAppService _contactAppService;
        private readonly IUserProvider _userProvider;

        public CategoryController(
            IUserProvider userProvider,
            ICategoryAppService categoryAppService,
            IContactAppService contactAppService,
            INotificationHandler<DomainNotification> notificationHandler,
            IEventHandler eventHandler) : base(notificationHandler, eventHandler, userProvider)
        {
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
            _categoryAppService = categoryAppService;
            _contactAppService = contactAppService;
        }

        [HttpGet]
        [Authorize(Policy = "CanReadData")]
        public IActionResult Get()
        {
            return Response(_categoryAppService.GetCategories(UserId));
        }

        [HttpGet]
        [Authorize(Policy = "CanReadData")]
        [Route("{categoryId:guid}/contacts")]
        public IActionResult GetContacts([FromRoute]Guid categoryId)
        {
            return Response(_contactAppService.GetContacts(UserId, categoryId));
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

        [HttpDelete]
        [Authorize(Policy = "CanDeleteData")]
        [Route("{categoryId:guid}")]
        public IActionResult Delete([FromRoute]Guid categoryId)
        {
            _categoryAppService.DeleteCategory(UserId, categoryId);
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "CanWriteData")]
        [Route("{categoryId:guid}")]
        public IActionResult Put([FromRoute]Guid categoryId, [FromBody]CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(categoryViewModel);
            }

            if (categoryId != categoryViewModel.Id)
            {
                return BadRequest();
            }

            _categoryAppService.UpdateCategory(categoryId, categoryViewModel);
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "CanReadData")]
        [Route("history/{categoryId:guid}")]
        public IActionResult GetEventHistory(Guid categoryId)
        {
            var customerHistoryData = _categoryAppService.GetEventHistory(categoryId);
            return Response(customerHistoryData);
        }
    }
}
