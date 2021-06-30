using App.Domain.core;
using App.Domain.core.Models;
using App.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Lector> Lectors { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<LecturesStudents> LecturesStudents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new HomeworkEntityConfiguration().Configure(modelBuilder.Entity<Homework>());
            new StudentEntityConfiguration().Configure(modelBuilder.Entity<Student>());
            new LectorEntityConfiguration().Configure(modelBuilder.Entity<Lector>());
            new LectureEntityConfiguration().Configure(modelBuilder.Entity<Lecture>());
            new LecturesStudentsEntityConfiguration().Configure(modelBuilder.Entity<LecturesStudents>());
        }
    }
}
