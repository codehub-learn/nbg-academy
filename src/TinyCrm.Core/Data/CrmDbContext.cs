using Microsoft.EntityFrameworkCore;

using TinyCrm.Core.Model;

namespace TinyCrm.Core.Data
{
    public class CrmDbContext : DbContext
    {
        public CrmDbContext(
            DbContextOptions<CrmDbContext> options) : base(options)
        { }

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
