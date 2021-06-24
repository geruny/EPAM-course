using App.Domain.core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace App.Infrastructure.Data.Configurations
{
    public class HomeWorkEntityConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.Property(h => h.Name).IsRequired();
            builder.Property(h => h.Date).HasColumnName("DateCompletion");

            builder.HasData(
                new Homework { Id = 1, StudentId = 1, Name = "Module 1", Date = new DateTime(2021, 01, 12), Mark = 4 },
                new Homework { Id = 2, StudentId = 1, Name = "Module 2", Date = new DateTime(2021, 01, 19), Mark = 5 },
                new Homework { Id = 3, StudentId = 2, Name = "Module 1", Date = new DateTime(2021, 01, 14), Mark = 3 },
                new Homework { Id = 4, StudentId = 2, Name = "Module 2", Date = new DateTime(2021, 02, 08), Mark = 0 },
                new Homework { Id = 5, StudentId = 3, Name = "Module 1", Date = new DateTime(2021, 01, 11), Mark = 4 },
                new Homework { Id = 6, StudentId = 3, Name = "Module 2", Date = new DateTime(2021, 02, 08), Mark = 0 }
            );
        }
    }
}
