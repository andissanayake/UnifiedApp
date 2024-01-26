using Api;
using Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Service;

namespace ApiTest
{
    public class Fixture : IDisposable
    {
        public Fixture()
        {
            WebApplication = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
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

                        ServiceDescriptor? serviceDescriptorTokenSettings = services.FirstOrDefault(descriptor =>
                            descriptor?.ServiceType == typeof(TokenSettings));
                        services.Remove(serviceDescriptorTokenSettings ?? default!);
                        var tokenSettings = new TokenSettings()
                        {
                            Issuer = "TestApi",
                            Audience = "TestAudience",
                            SecretKey = "SecretKeySecretKeySecretKeySecretKeySecretKey12345",
                            TokenExpireSeconds = 15,
                            RefreshTokenExpireSeconds = 25,
                        };
                        services.AddSingleton(tokenSettings);
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
