using ProductService.Application.ProductFeatures.Queries.GetFilteredSortedProducts;
using ProductService.DI;
using Service;
using Sieve.Services;

namespace ProductService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureSwagger();
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureServiceManager();
            builder.Services.AddAutoMapper(typeof(Program));
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
            builder.Services.AddHostedService<RabbitMqConsumerService>();

            //builder.Services.AddHostedService<RabbitMqListener>();
            var app = builder.Build();

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