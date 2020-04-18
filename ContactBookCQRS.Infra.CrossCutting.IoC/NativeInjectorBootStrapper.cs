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

namespace ContactBookCQRS.Infrastructure.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application Services
            services.AddScoped<IContactAppService, ContactAppService>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<CreateNewContactCommand, bool>, ContactCommandHandler>();

            // Infra - Persistence
            services.AddScoped<IContactsRepository, ContactsRepository>();
            services.AddScoped<IContactBookUnitOfWork, ContactBookUnitOfWork>();
            services.AddScoped<ContactBookContext>();
        }
    }
}
