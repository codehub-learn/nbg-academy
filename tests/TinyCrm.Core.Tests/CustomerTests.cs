using System;
using System.Linq;

using Xunit;

using TinyCrm.Core.Data;
using TinyCrm.Core.Model;

namespace TinyCrm.Core.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void Add_Customer_Success()
        {
            using var dbContext = new CrmDbContext();

            var customer = new Customer()
            {
                Address = "Athens",
                Name = "Dimitris",
                Surname = "Pnevmatikos",
                VatNumber = $"{Guid.NewGuid()}"
            };

            dbContext.Add(customer);
            dbContext.SaveChanges();

            var dbCustomer = dbContext.Set<Customer>()
                .Where(c => c.CustomerId == customer.CustomerId)
                .SingleOrDefault();

            Assert.NotNull(dbCustomer);
            Assert.Equal("Athens", dbCustomer.Address);
            Assert.Equal(customer.VatNumber, dbCustomer.VatNumber);
        }

        [Fact]
        public void Add_Customer_Duplicate_Vatnumber_Failure()
        {
            using var dbContext = new CrmDbContext();

            var customer = new Customer()
            {
                Address = "Athens",
                Name = "Dimitris",
                Surname = "Pnevmatikos",
                VatNumber = $"{Guid.NewGuid()}"
            };

            dbContext.Add(customer);
            dbContext.SaveChanges();

            customer = new Customer()
            {
                Address = "new address",
                Name = "Duplicate",
                VatNumber = customer.VatNumber
            };

            dbContext.Add(customer);
            Assert.ThrowsAny<Exception>(
                () => {
                    dbContext.SaveChanges();
                });
        }
    }
}
