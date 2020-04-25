using ContactBookCQRS.Domain.Models;
using ContactBookCQRS.Infra.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Infra.Persistence.Context
{
    public class ContactBookContext : DbContext
    {
        public ContactBookContext(DbContextOptions<ContactBookContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ContactBook> ContactBooks { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
