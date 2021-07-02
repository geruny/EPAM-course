using System.Collections.Generic;
using App.Domain.core.Models;
using App.Domain.Interfaces;
using App.Infrastructure.Business;
using App.Infrastructure.Data;
using App.Infrastructure.Data.Repositories;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "App", Version = "v1" });
            });

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection));

            services.AddScoped<IGenericBaseRepository<Student>, BaseRepository<Student>>();
            services.AddScoped<IGenericBaseRepository<Lector>, BaseRepository<Lector>>();
            services.AddScoped<IGenericBaseRepository<Lecture>, BaseRepository<Lecture>>();
            services.AddScoped<IGenericBaseRepository<Homework>, BaseRepository<Homework>>();

            services.AddScoped<ILecturesStudentsRepository, LecturesStudentsRepository>();
            services.AddScoped<IStudentsLectureService, StudentsLectureService>();
            services.AddScoped<IStudentHomeworksService, StudentHomeworksService>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<MailSender>();
            services.AddScoped<MessageSender>();
            services.AddTransient<ServiceFactory.ServiceResolver>(serviceProvider => serviceType =>
            {
                return serviceType switch
                {
                    ServiceFactory.ServiceType.MailSender => serviceProvider.GetService<MailSender>(),
                    ServiceFactory.ServiceType.MessageSender => serviceProvider.GetService<MessageSender>(),
                    _ => throw new KeyNotFoundException("Service not found")
                };
            });

            services.AddAutoMapper(typeof(MappingProfile));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "App v1"));

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}