using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ContactBookCQRS.Application.Interfaces;
using ContactBookCQRS.Application.ViewModels;
using ContactBookCQRS.Domain.Core.Bus;
using ContactBookCQRS.Domain.Core.Notifications;
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
        private readonly IUserProvider _userProvider;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager; 
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettings _appSettings;
        private readonly IContactBookAppService _contactBookAppService;

        public AccountController(
            IUserProvider userProvider,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor httpContextAccessor, 
            IContactBookAppService contactBookAppService,
            INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator) : base(notifications, mediator, userProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings.Value;
            _contactBookAppService = contactBookAppService;
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

            var user = new User(_httpContextAccessor)
            {
                UserName = userRegistration.Email,
                Email = userRegistration.Email
            };

            var result = await _userManager.CreateAsync(user, userRegistration.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    NotifyError(error.Code, error.Description);
                }

                return Response(userRegistration);
            }

            // User claim for write
            await _userManager.AddClaimAsync(user, new Claim("ContactBookOwner", "Write"));

            // Creates a ContactBook for the user
            _contactBookAppService.CreateContactBook(user.Id);

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

            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

            if (result.Succeeded)
            {
                var token = await GenerateJwt(userLogin.Email);
                var user = await _userManager.FindByEmailAsync(userLogin.Email);
                var contactBookId = await _contactBookAppService.GetContactBookIdByUser(user.Id);

                UserViewModel userViewModel = new UserViewModel();
                userViewModel.ContactBookId = contactBookId;
                userViewModel.Email = user.Email;
                userViewModel.Token = token;
                return Response(userViewModel);
            }

            NotifyError("Login", result.ToString());
            return Response(userLogin);
        }

        private async Task<string> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidAt,
                Expires = DateTime.UtcNow.AddHours(_appSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}