using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GatewayApi.Infrastructure.Clients.CustomersApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BaseGatewayController
    {
        private readonly ICustomersApiClient _customerClient;

        public CustomersController(ICustomersApiClient customerClient)
        {
            _customerClient = customerClient;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            try
            {
                var result = await _customerClient.GetCustomerAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }

        [HttpGet()]
        public async Task<ActionResult<ICollection<Customer>>> GetCustomers()
        {
            try
            {
                //var randomError = new Random().Next(0, 10);
                //if(randomError > 6)
                //{
                //    throw new Exception();
                //}
                var result = await _customerClient.GetCustomersAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }

        [HttpPost()]
        public async Task<ActionResult<IdResult>> CreateCustomer([FromBody]Customer customer)
        {
            try
            {
                var result = await _customerClient.PostCustomerAsync(customer);
                return Ok(result);
            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IdResult>> UpdateCustomer(int id, [FromBody]Customer customer)
        {
            try
            {
                var result = await _customerClient.PostCustomerAsync(customer);
                return Ok(result);
            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _customerClient.DeleteCustomerAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }
    }
}