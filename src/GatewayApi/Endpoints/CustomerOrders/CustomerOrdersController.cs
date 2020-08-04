using System;
using System.Linq;
using System.Threading.Tasks;
using GatewayApi.Controllers;
using GatewayApi.Controllers.CustomerOrders;
using GatewayApi.Infrastructure.Clients.CustomersApi;
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
        private readonly ICustomersApiClient _customersClient;

        public CustomerOrdersController(ICustomerOrdersService customerOrdersService, ICustomersApiClient customersClient)
        {
            _service = customerOrdersService;
            _customersClient = customersClient;
        }

        [HttpGet("customers/{customerId}/orders")]
        public async Task<ActionResult<CustomerOrdersViewModel>> GetCustomerOrders(int customerId)
        {
            try
            {
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

        [HttpGet("customers")]
        public async Task<ActionResult<CustomerOrdersViewModel>> GetCustomerList(int customerId)
        {
            try
            {
                //var randomError = new Random().Next(0, 10);
                //if(randomError > 6)
                //{
                //    throw new Exception();
                //}
                var result = await _customersClient.GetCustomersAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }
    }
}