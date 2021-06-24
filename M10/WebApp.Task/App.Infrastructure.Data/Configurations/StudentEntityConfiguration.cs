using App.Domain.core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Data.Configurations
{
    public class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(s => s.Name).IsRequired();
            builder.Property(h => h.Date).HasColumnName("DateBirth");

            builder.HasData(
                new Student { Id = 1, Name = "Tom", Date = new DateTime(1995, 03, 23), Email = "Tom@Epam.com", PhoneNumber = "89112329485" },
                new Student { Id = 2, Name = "Alice", Date = new DateTime(2000, 11, 03), Email = "Alice@Epam.com", PhoneNumber = "89522281488" },
                new Student { Id = 3, Name = "Sam", Date = new DateTime(1995, 07, 14), Email = "Sam@Epam.com", PhoneNumber = "88005553535" }
                );
        }
    }
}
