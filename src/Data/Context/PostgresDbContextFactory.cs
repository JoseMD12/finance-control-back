using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data.Context
{
    public class PostgresDbContextFactory
    : IDesignTimeDbContextFactory<PostgresDbContext>
    {
        public PostgresDbContextFactory() { }
        public PostgresDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostgresDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5428;Database=postgres;UserId=joseph;Password=061202");
            return new PostgresDbContext(optionsBuilder.Options);
        }
    }
}