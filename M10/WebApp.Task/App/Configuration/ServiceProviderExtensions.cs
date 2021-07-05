using System.Collections.Generic;
using App.Domain.core.Models;
using App.Domain.Interfaces;
using App.Infrastructure.Business;
using App.Infrastructure.Business.Options;
using App.Infrastructure.Data;
using App.Infrastructure.Data.Repositories;
using App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace App.Configuration
{
    public static class ServiceProviderExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGenericBaseRepository<Student>, BaseRepository<Student>>();
            services.AddScoped<IGenericBaseRepository<Lector>, BaseRepository<Lector>>();
            services.AddScoped<IGenericBaseRepository<Lecture>, BaseRepository<Lecture>>();
            services.AddScoped<IGenericBaseRepository<Homework>, BaseRepository<Homework>>();
            services.AddScoped<ILecturesStudentsRepository, LecturesStudentsRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "App", Version = "v1" });
            });

            services.AddScoped<IStudentsLectureService, StudentsLectureService>();
            services.AddScoped<IStudentHomeworksService, StudentHomeworksService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IReporter, Reporter>();
            services.AddScoped<MailSender>();
            services.AddScoped<MessageSender>();
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddTransient<ServiceFactory.ServiceResolver>(serviceProvider => serviceType =>
            {
                return serviceType switch
                {
                    ServiceFactory.ServiceType.MailSender => serviceProvider.GetService<MailSender>(),
                    ServiceFactory.ServiceType.MessageSender => serviceProvider.GetService<MessageSender>(),
                    _ => throw new KeyNotFoundException("Service not found")
                };
            });
        }

        public static void AddConfigurations(this IServiceCollection services, IConfiguration config)
        {
            var connection = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection));

            services.Configure<StudentServiceOptions>(config.GetSection(key: nameof(StudentServiceOptions)));
        }
    }
}