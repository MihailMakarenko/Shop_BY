using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.ProductFeatures.Queries.GetFilteredSortedProducts;
using ProductService.DI;
using Repository;
using Service.Behavior;
using Service.Commands.ProductCommands.CreateProduct;
using Service.Commands.ProductCommands.UpdateProduct;
using Sieve.Services;

namespace ProductService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateProductCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly);
            });

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.ConfigureSwagger();
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureServiceManager();
            builder.Services.ConfigureFluentValidation();

            builder.Services.AddControllers()
                .AddNewtonsoftJson()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

            builder.Services.ConfigureJWT(builder.Configuration);
            builder.Services.AddScoped<SieveProcessor, ApplicationSieveProcessor>();
            builder.Services.ConfigureCors();
            builder.Services.ConfigureIISIntegration();
            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<ISieveProcessor, SieveProcessor>();
            builder.Services.ConfigureRabbitMqConsumers();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var skipMigration = Environment.GetEnvironmentVariable("SKIP_MIGRATION");

                if (string.IsNullOrEmpty(skipMigration))
                {
                    dbContext.Database.Migrate();
                }
            }

            app.ConfigureExceptionHandler();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserService API v1");
                c.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}