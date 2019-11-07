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
                       
            if (result.StatusCode == 404)
            {
                return NotFound();
            }
            return Ok(result.Content);
        }
    }
}