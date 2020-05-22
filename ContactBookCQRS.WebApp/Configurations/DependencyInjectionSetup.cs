using ContactBookCQRS.Application.Interfaces;
using ContactBookCQRS.Application.Services;
using ContactBookCQRS.Domain.CommandHandlers;
using ContactBookCQRS.Domain.Commands;
using ContactBookCQRS.Domain.Events;
using ContactBookCQRS.Domain.Identity;
using ContactBookCQRS.Domain.Notifications;
using ContactBookCQRS.Domain.Persistence;
using ContactBookCQRS.Infra.CrossCutting.Bus;
using ContactBookCQRS.Infra.CrossCutting.Identity.Authorization;
using ContactBookCQRS.Infra.CrossCutting.Identity.Models;
using ContactBookCQRS.Infra.CrossCutting.Identity.Services;
using ContactBookCQRS.Infra.Persistence.Repository;
using ContactBookCQRS.Infra.Persistence.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ContactBookCQRS.WebApp.Configurations
{
    public static class DependencyInjectionSetup
    {
        public static void AddDependencyInjectionSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // Domain Bus (Mediator)
            services.AddScoped<ICommandHandler, InMemoryBus>();
            services.AddScoped<IEventHandler, InMemoryBus>();

            // Application Services
            services.AddScoped<IContactAppService, ContactAppService>();
            services.AddScoped<IContactBookAppService, ContactBookAppService>();
            services.AddScoped<ICategoryAppService, CategoryAppService>();
            services.AddScoped<IAccountAppService, AccountAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<CreateNewContactBookCommand, bool>, ContactBookCommandHandler>();
            services.AddScoped<IRequestHandler<CreateNewCategoryCommand, bool>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCategoryCommand, bool>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteCategoryCommand, bool>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<CreateNewContactCommand, bool>, ContactCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteContactCommand, bool>, ContactCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateContactCommand, bool>, ContactCommandHandler>();

            // Infra - Persistence
            services.AddScoped<IContactsRepository, ContactsRepository>();
            services.AddScoped<IContactBooksRepository, ContactBooksRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IContactBookUnitOfWork, ContactBookUnitOfWork>();

            // Infra Identity - Persistence
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStoreService, EventStoreService>();

            // Infra - Identity
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUser, User>();
        }
    }
}
