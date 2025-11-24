
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    public class PostgresContext(DbContextOptions<PostgresContext> options) : DbContext(options)
    {
        public static void CreateDbContext(IServiceCollection builder, IConfiguration configuration)
        {
            builder.AddDbContext<PostgresContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configuration.User());

            base.OnModelCreating(modelBuilder);
        }
    }
}