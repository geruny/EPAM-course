using App.Domain.core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Configurations
{
    public class LecturesStudentsEntityConfiguration : IEntityTypeConfiguration<LecturesStudents>
    {
        public void Configure(EntityTypeBuilder<LecturesStudents> builder)
        {
            builder.HasKey(ls => new { ls.LectureId, ls.StudentId });

            builder.HasOne(l => l.Lecture)
                .WithMany(ls => ls.LecturesStudents)
                .HasForeignKey(l => l.LectureId);

            builder.HasOne(s => s.Student)
                .WithMany(ls => ls.LecturesStudents)
                .HasForeignKey(s => s.StudentId);

            builder.HasData(
                new LecturesStudents { LectureId = 1, StudentId = 1 },
                new LecturesStudents { LectureId = 1, StudentId = 2 },
                new LecturesStudents { LectureId = 1, StudentId = 3 },
                new LecturesStudents { LectureId = 2, StudentId = 1 },
                new LecturesStudents { LectureId = 2, StudentId = 2 }
            );
        }
    }
}
