using ContactBookCQRS.Domain.Aggregates;
using ContactBookCQRS.Infra.CrossCutting.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactBookCQRS.Infra.Persistence.Configurations
{
    public class ContactBookConfiguration : IEntityTypeConfiguration<ContactBook>
    {
        public void Configure(EntityTypeBuilder<ContactBook> builder)
        {
            builder.ToTable("ContactBooks", "dbo");

            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.HasMany(e => e.Categories);
        }
    }
}
