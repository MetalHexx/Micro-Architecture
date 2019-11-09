using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CbInsights.Clients;
using CbInsights.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CbInsights.GatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomersClient _customerClient;

        public CustomersController(CustomersClient customerClient)
        {
            _customerClient = customerClient;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var result = await _customerClient.GetCustomerByIdAsync(id);

            if (result.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }
            return Ok(result.Content);
        }

        [HttpGet()]
        public async Task<ActionResult<Customer>> GetCustomers()
        {
            var result = await _customerClient.GetCustomersAsync();

            if (result.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }
            return Ok(result.Content);
        }

        [HttpPost()]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody]Customer customer)
        {
            var result = await _customerClient.CreateCustomerAsync(customer);
            return Ok(result.Content);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(int id, [FromBody]Customer customer)
        {
            var result = await _customerClient.UpdateCustomerAsync(customer);
            if (result.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }
            return Ok(result.Content);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var result = await _customerClient.DeleteCustomerAsync(id);

            if (result.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}