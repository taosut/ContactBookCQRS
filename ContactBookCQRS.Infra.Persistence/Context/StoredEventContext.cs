using ContactBookCQRS.Domain.Events;
using ContactBookCQRS.Infra.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Infra.Persistence.Context
{
    public class StoredEventContext : DbContext
    {
        public StoredEventContext(DbContextOptions<StoredEventContext> options) : base(options) 
        {
            Database.Migrate();
        }

        public DbSet<StoredEvent> StoredEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredEventConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
