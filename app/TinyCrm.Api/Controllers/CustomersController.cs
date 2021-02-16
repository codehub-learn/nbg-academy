using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TinyCrm.Core.Constants;
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
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customers.GetByIdAsync(id);

            return Json(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Register(
            [FromBody] RegisterCustomerOptions options)
        {
            var result = await _customers.RegisterAsync(options);

            if (result.Code != ResultCode.Success) {
                return StatusCode(result.Code, result.ErrorMessage);
            }

            return Json(result);
        }
    }
}
