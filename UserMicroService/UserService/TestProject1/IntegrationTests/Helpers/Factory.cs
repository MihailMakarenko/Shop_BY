using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Service.Contract;

namespace UserService.Tests.IntegrationTests.Helpers
{
    public class Factory<TProgram> : WebApplicationFactory<Program> where TProgram : Program
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.UseEnvironment("Testing");

            builder.ConfigureAppConfiguration((context, config) =>
            {
                Environment.SetEnvironmentVariable("SKIP_MIGRATION", "true");
            });

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IEmailService));
                services.Remove(descriptor!);

                descriptor = new(typeof(IEmailService), typeof(TestEmailService), ServiceLifetime.Scoped);
                services.Add(descriptor);

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer("Server=localhost;Database=db_userTest7;Trusted_Connection=True;TrustServerCertificate=True;");
                });

                using var scope = services.BuildServiceProvider().CreateScope();
                using var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                appContext.Database.EnsureCreated();
                SeedData.SeedTestData(appContext);
            });
        }
   
    }
}
