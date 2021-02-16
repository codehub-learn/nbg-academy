using Microsoft.EntityFrameworkCore;

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TinyCrm.Core.Constants;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services.Options;
using Npoi.Mapper;
using System.Globalization;

namespace TinyCrm.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private CrmDbContext _dbContext;

        public CustomerService(CrmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            var customer = await _dbContext.Set<Customer>()
                .Where(c => c.CustomerId == customerId)
                .SingleOrDefaultAsync();

            return customer;
        }

        public Result<List<Customer>> ParseFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) {
                return new Result<List<Customer>>()
                {
                    Code = ResultCode.BadRequest,
                    ErrorMessage = $"Invalid file path {path}"
                };
            }

            if (!File.Exists(path)) {
                return new Result<List<Customer>>()
                {
                    Code = ResultCode.NotFound,
                    ErrorMessage = $"File {path} was not found"
                };
            }

            var listOfCustomers = new List<Customer>();

            var mapper = new Mapper(path);

            mapper.Map<Customer>(3, $"{nameof(Customer.TotalGross)}",
                (columnInfo, customer) => {

                    var gross = decimal.Parse(
                        columnInfo.CurrentValue.ToString(), CultureInfo.InvariantCulture);

                    ((Customer) customer).TotalGross = gross;

                    return true;
                });

            var results = mapper
                .Take<Customer>()
                .Select(row => row.Value)
                .ToList();

            return new Result<List<Customer>>()
            {
                Code = ResultCode.Success,
                Data = results
            };
        }

        public async Task<Result<Customer>> RegisterAsync(RegisterCustomerOptions options)
        {
            if (string.IsNullOrWhiteSpace(options?.VatNumber)) {
                return new Result<Customer>()
                {
                    ErrorMessage = "VatNumber is empty",
                    Code = Constants.ResultCode.BadRequest
                };
            }

            if (options.VatNumber.Length != 9) {
                return new Result<Customer>()
                {
                    ErrorMessage = "VatNumber requires 9 digits",
                    Code = Constants.ResultCode.BadRequest
                };
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

            try {
                await _dbContext.SaveChangesAsync();
            } catch (Exception) {
                return new Result<Customer>()
                {
                    Code = Constants.ResultCode.InternalServerError,
                    ErrorMessage = "Customer could not be saved"
                };
            }

            return new Result<Customer>()
            {
                Code = Constants.ResultCode.Success,
                Data = customer
            };
        }
    }
}
