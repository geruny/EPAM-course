using App.Domain.core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace App.Infrastructure.Data.Configurations
{
    public class LectorEntityConfiguration : IEntityTypeConfiguration<Lector>
    {
        public void Configure(EntityTypeBuilder<Lector> builder)
        {
            builder.Property(l => l.Name).IsRequired();
            builder.Property(h => h.Date).HasColumnName("DateBirth");

            builder.HasData(
                 new Lector { Id = 1, Name = "Vasiliy", Date = new DateTime(1965, 07, 10), Email = "Vasiliy@Epam.com", PhoneNumber = "88001234455" },
                 new Lector { Id = 2, Name = "Boris", Date = new DateTime(1943, 09, 01), Email = "Boris@Epam.com", PhoneNumber = "89043223443" }
                 );
        }
    }
}
