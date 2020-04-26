using ContactBookCQRS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace ContactBookCQRS.Infra.CrossCutting.Identity.Models
{
    /// <summary>
    /// Application User
    /// </summary>
    public class User : IdentityUser, IUser 
    {
        public string Name => GetName();

        private readonly IHttpContextAccessor _accessor;

        private User()
        {
        }

        public User(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        private string GetName()
        {
            return _accessor.HttpContext.User.Identity.Name ??
                   _accessor.HttpContext.User.Claims
                   .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }

        public string GetId()
        {
            return Id;
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }
}
