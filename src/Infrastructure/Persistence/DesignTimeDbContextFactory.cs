using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Infrastructure.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PostgresContext>
    {
        public PostgresContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration;
            try
            {
                configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"You should try running: \n\n dotnet ef database update -p Infrastructure -s Api \n\nMake sure the working directory is the root of the solution.");
                throw new Exception("Failed to build configuration", ex);
            }

            var optionsBuilder = new DbContextOptionsBuilder<PostgresContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            return new PostgresContext(optionsBuilder.Options);
        }

    }
}
