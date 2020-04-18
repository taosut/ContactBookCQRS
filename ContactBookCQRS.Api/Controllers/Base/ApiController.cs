using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactBookCQRS.Domain.Core.Bus;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactBookCQRS.Api.Controllers.Base
{
    public abstract class ApiController : ControllerBase
    {
        private readonly IMediatorHandler _mediator;

        protected ApiController(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        protected bool IsValidOperation()
        {
            return true;
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
                errors = string.Empty
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
    }
}
