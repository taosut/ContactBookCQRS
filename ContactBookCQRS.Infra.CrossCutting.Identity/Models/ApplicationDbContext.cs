using ContactBookCQRS.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContactBookCQRS.Infra.CrossCutting.Identity.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
