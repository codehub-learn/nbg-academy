using Microsoft.EntityFrameworkCore;

using TinyCrm.Core.Model;

namespace TinyCrm.Core.Data
{
    public class CrmDbContext : DbContext
    {
        const string connectionString =
            "Server=localhost;Database=crmdb;User Id=sa;Password=admin!@#123;";

        public CrmDbContext()
        { }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(connectionString,
                options => {
                    options.MigrationsAssembly("ConsoleApp2");
                });
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .ToTable("Customer");

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.VatNumber)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .ToTable("Order");
        }
    }
}
