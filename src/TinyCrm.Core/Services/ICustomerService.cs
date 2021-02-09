using TinyCrm.Core.Model;
using TinyCrm.Core.Services.Options;

namespace TinyCrm.Core.Services
{
    public interface ICustomerService
    {
        public Customer Register(RegisterCustomerOptions options);
        public Customer GetById(int customerId);
    }
}
