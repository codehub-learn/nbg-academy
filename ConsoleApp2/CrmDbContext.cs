using Microsoft.EntityFrameworkCore;

namespace ConsoleApp2
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

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .ToTable("Customer");
        }
    }
}
