using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CbInsights.GatewayApi.Clients;
using CbInsights.GatewayApi.Clients.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CbInsights.GatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BaseGatewayController
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
            return GetResult(result);
        }

        [HttpGet()]
        public async Task<ActionResult<Customer>> GetCustomers()
        {
            var result = await _customerClient.GetCustomersAsync();
            return GetResult(result);
        }

        [HttpPost()]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody]Customer customer)
        {
            var result = await _customerClient.CreateCustomerAsync(customer);
            return GetResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(int id, [FromBody]Customer customer)
        {
            var result = await _customerClient.UpdateCustomerAsync(customer);
            return GetResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var result = await _customerClient.DeleteCustomerAsync(id);
            return GetResult(result);
        }
    }
}