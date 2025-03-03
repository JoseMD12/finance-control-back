using Domain.Entities.Sheet;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class PostgresDbContext(DbContextOptions<PostgresDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<SheetTemplateEntity> SheetTemplates { get; set; }
        public DbSet<ColumnTypeValuePairEntity> ColumnTypeValuePairs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>().HasIndex(x => x.Email).IsUnique();

            modelBuilder.Entity<SheetTemplateEntity>().HasIndex(x => x.TemplateName).IsUnique();
            modelBuilder.Entity<SheetTemplateEntity>().HasMany(x => x.Columns).WithOne(x => x.SheetTemplate).HasForeignKey(x => x.SheetTemplateId);

            modelBuilder.Entity<ColumnTypeValuePairEntity>().HasOne(x => x.SheetTemplate).WithMany(x => x.Columns).HasForeignKey(x => x.SheetTemplateId);
        }
    }

}