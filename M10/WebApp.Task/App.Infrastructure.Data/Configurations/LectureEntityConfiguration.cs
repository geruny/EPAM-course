using App.Domain.core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace App.Infrastructure.Data.Configurations
{
    public class LectureEntityConfiguration : IEntityTypeConfiguration<Lecture>
    {
        public void Configure(EntityTypeBuilder<Lecture> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Name).IsRequired();

            builder.HasOne(l => l.Lector)
                .WithMany(l => l.Lectures).HasForeignKey(l => l.LectorId);
            builder.HasMany(l => l.LecturesStudents)
                .WithOne(l => l.Lecture).HasForeignKey(l => l.LectureId);

            builder.HasData(
                new Lecture { Id = 1, LectorId = 2, Name = "Module 1", DateEvent = new DateTime(2021, 02, 01) },
                new Lecture { Id = 2, LectorId = 1, Name = "Module 2", DateEvent = new DateTime(2021, 02, 08) }
            );
        }
    }
}
