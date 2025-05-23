using Microsoft.EntityFrameworkCore;
using Repository;

namespace UserService.DI
{
    public static class ServiceExtensions
    {
        // Configuring CORS
        public static void ConfigureCors(this IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
            });
        }

        // Configuring IIS
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
            });
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opts =>
  opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b =>
 b.MigrationsAssembly("CompanyEmployees")));

        }

    }
}
