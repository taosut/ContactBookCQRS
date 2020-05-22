using ContactBookCQRS.Infra.CrossCutting.Identity.Models;
using ContactBookCQRS.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBookCQRS.WebApp.Configurations
{
    public static class DatabaseSetup
    {
        public static void AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            string connString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContextPool<IdentityContext>(options =>
                options.UseSqlServer(connString));

            services.AddDbContextPool<ContactBookContext>(options =>
                options.UseSqlServer(connString));

            services.AddDbContextPool<StoredEventContext>(options =>
                options.UseSqlServer(connString));
        }
    }
}
