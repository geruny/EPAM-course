using System.Linq;
using App.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace App.Integration.Tests
{
    [SetUpFixture]
    public static class TestsConfigurationSetUp
    {
        public static WebApplicationFactory<Startup> WebConfigSetUp()
        {
            var webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
                builder.ConfigureServices(services =>
                    {
                        var dbContextDescriptor = services.SingleOrDefault(d =>
                            d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                        services.Remove(dbContextDescriptor);

                        services.AddDbContext<ApplicationDbContext>(options =>
                            options.UseInMemoryDatabase("db"));
                    }
                ));

            return webHost;
        }
    }
}