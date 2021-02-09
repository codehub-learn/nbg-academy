using System.Linq;

using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services.Options;

namespace TinyCrm.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private CrmDbContext _dbContext;

        public CustomerService(CrmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Customer GetById(int customerId)
        {
            return _dbContext.Set<Customer>()
                .Where(c => c.CustomerId == customerId)
                .SingleOrDefault();
        }

        public Customer Register(RegisterCustomerOptions options)
        {
            if (string.IsNullOrWhiteSpace(options?.VatNumber)) {
                return null;
            }

            if (options.VatNumber.Length != 9) {
                return null;
            }

            if (string.IsNullOrWhiteSpace(options.Surname) ||
              string.IsNullOrWhiteSpace(options.Name)) {
                return null;
            }

            var customer = new Customer()
            {
                Name = options.Name,
                Surname = options.Surname,
                VatNumber = options.VatNumber
            };

            _dbContext.Add(customer);
            _dbContext.SaveChanges();

            return customer;
        }
    }
}
