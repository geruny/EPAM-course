using App.Domain.core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace App.Infrastructure.Data.Configurations
{
    public class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).IsRequired();

            builder.HasMany(s => s.Homeworks)
                .WithOne(h => h.Student).HasForeignKey(h => h.StudentId);
            builder.HasMany(s => s.LecturesStudents)
                .WithOne(l => l.Student).HasForeignKey(l => l.StudentId);

            builder.HasData(
                new Student { Id = 1, Name = "Tom", DateBirth = new DateTime(1995, 03, 23), Email = "Tom@Epam.com", PhoneNumber = "89112329485" },
                new Student { Id = 2, Name = "Alice", DateBirth = new DateTime(2000, 11, 03), Email = "Alice@Epam.com", PhoneNumber = "89522281488" },
                new Student { Id = 3, Name = "Sam", DateBirth = new DateTime(1995, 07, 14), Email = "Sam@Epam.com", PhoneNumber = "88005553535" }
                );
        }
    }
}
