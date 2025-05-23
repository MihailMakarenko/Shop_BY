using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Repository;

namespace WebApplication1.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();

            var builder = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("UserService"));

            return new AppDbContext(builder.Options);
        }
    }
}
