using System;
using System.Linq;
using System.Threading.Tasks;
using GatewayApi.Clients;
using GatewayApi.Controllers;
using GatewayApi.Controllers.CustomerOrders;
using GatewayApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GatewayApi.Infrastructure
{   
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrdersController : BaseGatewayController
    {
        private readonly ICustomerOrdersService _service;

        public CustomerOrdersController(ICustomerOrdersService customerOrdersService){
            _service = customerOrdersService;
        }

        [HttpGet("customers/{customerId}/orders")]
        public async Task<ActionResult<CustomerOrdersViewModel>> GetCustomerOrders(int customerId)
        {
            try
            {
                var randomError = new Random().Next(0, 10);
                if (randomError > 6)
                {
                    throw new Exception();
                }

                var result = await _service.GetCustomerOrdersAsync(customerId);
                var customerOrders = new CustomerOrdersViewModel
                (
                    result.CustomerResult,
                    result.OrderResult,
                    result.ProductResult
                );
                return Ok(customerOrders);
            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }            
        }        
    }
}