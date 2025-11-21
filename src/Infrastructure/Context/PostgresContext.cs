using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Context
{
    public class PostgresContext : DbContext
    {
        public static void CreateDbContext(IServiceCollection builder, IConfiguration configuration)
        {
            builder.AddDbContext<PostgresContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}