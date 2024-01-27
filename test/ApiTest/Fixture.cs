using Api;
using Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTest
{
    public class Fixture : IDisposable
    {
        public Fixture()
        {
            WebApplication = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Test");
                    builder.ConfigureServices(services =>
                    {
                        var context = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(ApplicationDbContext));
                        if (context != null)
                        {
                            services.Remove(context);
                            var options = services.Where(r => (r.ServiceType == typeof(DbContextOptions))
                              || (r.ServiceType.IsGenericType && r.ServiceType.GetGenericTypeDefinition() == typeof(DbContextOptions<>))).ToArray();
                            foreach (var option in options)
                            {
                                services.Remove(option);
                            }
                        }

                        // Add a new registration for ApplicationDbContext with an in-memory database
                        services.AddDbContext<ApplicationDbContext>(options =>
                        {
                            // Provide a unique name for your in-memory database
                            options.UseInMemoryDatabase("InMemoryDatabaseName");
                        });
                    });
                });
        }

        public WebApplicationFactory<Program> WebApplication { get; }

        public void Dispose()
        {
            WebApplication?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
