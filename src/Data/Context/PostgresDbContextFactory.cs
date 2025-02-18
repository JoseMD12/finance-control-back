using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data.Context
{
    public class PostgresDbContextFactory : IDesignTimeDbContextFactory<PostgresDbContext>
    {
        private readonly IConfiguration _configuration;

        public PostgresDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PostgresDbContextFactory()
        {
            var basePath = Directory.GetCurrentDirectory();
            var appsettingsPath = Path.GetFullPath(Path.Combine(basePath, "../../src/Application/appsettings.json"));

            if (!File.Exists(appsettingsPath))
            {
                throw new FileNotFoundException($"appsettings.json not found at: {appsettingsPath}");
            }

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(appsettingsPath)!)
                .AddJsonFile(Path.GetFileName(appsettingsPath))
                .Build();
        }

        public PostgresDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostgresDbContext>();
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            return new PostgresDbContext(optionsBuilder.Options);
        }
    }
}