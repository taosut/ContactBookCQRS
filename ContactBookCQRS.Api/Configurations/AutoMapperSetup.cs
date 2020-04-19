using System;
using AutoMapper;
using ContactBookCQRS.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ContactBookCQRS.Api.Configurations
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToViewModelProfile), typeof(ViewModelToDomainProfile));
        }
    }
}
