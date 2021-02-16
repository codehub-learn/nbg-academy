using System.Collections.Generic;
using System.Threading.Tasks;

using TinyCrm.Core.Model;
using TinyCrm.Core.Services.Options;

namespace TinyCrm.Core.Services
{
    public interface ICustomerService
    {
        public Task<Result<Customer>> RegisterAsync(RegisterCustomerOptions options);
        public Task<Customer> GetByIdAsync(int customerId);
        public Result<List<Customer>> ParseFile(string path);
    }
}
