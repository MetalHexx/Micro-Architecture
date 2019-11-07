using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CbInsights.CustomerApi.Models;
using CbInsights.CustomerApi.Repository;
using CbInsights.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CbInsights.CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRespository _customerRepo;

        public CustomersController(ICustomerRespository customerRepo)
        {
            _customerRepo = customerRepo;
        }
        [HttpGet()]
        public async Task<ActionResult<Customer>> GetCustomers()
        {
            var customers = _customerRepo.GetCustomers();

            if (customers == null || customers.ToList().Count == 0)
            {
                return NotFound();
            }
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = _customerRepo.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerCreateResult>> CreateCustomer([FromBody] Customer customer)
        {
            var id = _customerRepo.InsertCustomer(customer);
            return Ok(new CustomerCreateResult { Id = id });
        }

        [HttpPut("{id}")]
        public async Task UpdateCustomer([FromRoute]int id, [FromBody] Customer customer)
        {
            _customerRepo.UpdateCustomer(customer);
        }

        [HttpDelete("{id}")]
        public async Task DeleteCustomer(int id)
        {
            _customerRepo.DeleteCustomer(id);
        }
    }
}