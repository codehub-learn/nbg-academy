using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrm.Core.Constants;

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
        public async Task<Customer> RegisterCustomer_Success()
        {
            var options = new Services.Options.RegisterCustomerOptions() {
                Name = "Dimitris",
                Surname = "Pnevmatikos",
                VatNumber = $"{System.Guid.NewGuid()}" // Fix
            };

            var customer = (await _customers.RegisterAsync(options))?.Data;

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
            var customer = RegisterCustomer_Success();

            _dbContext.Remove(customer);
            _dbContext.SaveChanges();
        }

        [Fact]
        public void ParseExcelFile_Success()
        {
            var result = _customers.ParseFile(
                @"C:\Users\JIM_S\devel\temp\ConsoleApp2\tests\TinyCrm.Core.Tests\bin\Debug\net5.0\Files\Book1.xlsx");

            Assert.Equal(ResultCode.Success, result.Code);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);

            var customer = result.Data[0];
            Assert.Equal("Dimitris", customer.Name);
            Assert.Equal("123456789", customer.VatNumber);
            Assert.Equal(1500.33M, customer.TotalGross);
        }
    }
}
