using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrm.Core.Services.Options;

namespace TinyCrm.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customers;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(
            ILogger<CustomersController> logger,
            ICustomerService customers)
        {
            _logger = logger;
            _customers = customers;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var customer = _customers.GetById(id);

            return Json(customer);
        }

        [HttpPost]
        public IActionResult Register(
            [FromBody] RegisterCustomerOptions options)
        {
            var customer = _customers.Register(options);

            return Json(customer);
        }
    }
}
