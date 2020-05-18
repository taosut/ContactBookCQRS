using System;
using System.Text;
using ContactBookCQRS.Infra.CrossCutting.Identity.Authorization;
using ContactBookCQRS.Infra.CrossCutting.Identity.Models;
using ContactBookCQRS.Infra.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ContactBookCQRS.WebApp.Configurations
{
    public static class IdentitySetup
    {
        public static void AddIdentitySetup(this IServiceCollection services, 
            IConfiguration configuration)
        {
            if (null == services) 
                throw new ArgumentNullException(nameof(services));

            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddUserStore<UserStore<User, Role, IdentityContext, Guid>>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            // JWT Setup
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.ValidAt,
                    ValidIssuer = appSettings.Issuer
                };
            });

            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });
        }

        public static void AddAuthSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanWriteData", policy => policy.Requirements.Add(new ClaimRequirement("CanWriteData", "Write")));
                options.AddPolicy("CanDeleteData", policy => policy.Requirements.Add(new ClaimRequirement("CanDeleteData", "Delete")));
            });
        }
    }
}
