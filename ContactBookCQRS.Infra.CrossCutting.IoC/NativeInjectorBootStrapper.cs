using ContactBookCQRS.Application.Interfaces;
using ContactBookCQRS.Application.Services;
using ContactBookCQRS.Domain.CommandHandlers;
using ContactBookCQRS.Domain.Commands;
using ContactBookCQRS.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ContactBookCQRS.Infra.Persistence.UnitOfWork;
using ContactBookCQRS.Infra.Persistence.Context;
using ContactBookCQRS.Domain.Core.Bus;
using ContactBookCQRS.Infra.CrossCutting.Bus;
using ContactBookCQRS.Infra.Persistence.Repository;
using ContactBookCQRS.Domain.Core.Notifications;
using Microsoft.AspNetCore.Authorization;
using ContactBookCQRS.Infra.CrossCutting.Identity.Authorization;
using ContactBookCQRS.Infra.CrossCutting.Identity.Models;

namespace ContactBookCQRS.Infrastructure.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application Services
            services.AddScoped<IContactAppService, ContactAppService>();
            services.AddScoped<IContactBookAppService, ContactBookAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<CreateNewContactBookCommand, bool>, ContactBookCommandHandler>();
            services.AddScoped<IRequestHandler<CreateNewContactCommand, bool>, ContactCommandHandler>();

            // Infra - Persistence
            services.AddScoped<IContactsRepository, ContactsRepository>();
            services.AddScoped<IContactBooksRepository, ContactBooksRepository>();
            services.AddScoped<IContactBookUnitOfWork, ContactBookUnitOfWork>();
            services.AddScoped<ContactBookContext>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
