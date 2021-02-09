using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

using TinyCrm.Core.Model;
using TinyCrm.Core.Services;

namespace TinyCrm.Core.Tests
{
    public class CustomerTests : IClassFixture<TinyCrmFixture>
    {
        private ICustomerService _customers;
        private Data.CrmDbContext _dbContext;

        public CustomerTests(TinyCrmFixture fixture)
        {
            _customers = fixture.Scope.ServiceProvider
                .GetRequiredService<ICustomerService>();
            _dbContext = fixture.Scope.ServiceProvider
                .GetRequiredService<Data.CrmDbContext>();
        }

        [Fact]
        public Customer Add_Customer_Success()
        {
            var options = new Services.Options.RegisterCustomerOptions() {
                Name = "Dimitris",
                Surname = "Pnevmatikos",
                VatNumber = $"{System.Guid.NewGuid()}"
            };

            var customer = _customers.Register(options);

            Assert.NotNull(customer);

            var savedCustomer = _dbContext.Set<Customer>()
                .Where(c => c.CustomerId == customer.CustomerId)
                .SingleOrDefault();
            Assert.NotNull(savedCustomer);
            Assert.Equal(options.Name, savedCustomer.Name);
            Assert.Equal(options.Surname, savedCustomer.Surname);
            Assert.Equal(options.VatNumber, savedCustomer.VatNumber);

            return customer;
        }

        [Fact]
        public void Delete_Customer_Success()
        {
            var customer = Add_Customer_Success();

            _dbContext.Remove(customer);
            _dbContext.SaveChanges();
        }
    }
}
