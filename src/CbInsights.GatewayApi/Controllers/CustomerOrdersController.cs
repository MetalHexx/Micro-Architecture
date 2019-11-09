using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CbInsights.Clients;
using CbInsights.GatewayApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CbInsights.GatewayApi.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrdersController : ControllerBase
    {
        private readonly CustomersClient _customersClient;
        private readonly OrdersClient _ordersClient;
        private readonly ProductsClient _productsClient;

        public CustomerOrdersController(
            CustomersClient customersClient, 
            OrdersClient ordersClient, 
            ProductsClient products)
        {
            _customersClient = customersClient;
            _ordersClient = ordersClient;
            _productsClient = products;
        }
        [HttpGet("customers/{customerId}")]
        public async Task<ActionResult<CustomerOrdersModel>> GetCustomerOrders(int customerId)
        {
            var customerTask = Task.Run(() => _customersClient.GetCustomerByIdAsync(customerId));
            var orderTask = Task.Run(() => _ordersClient.GetCustomerOrdersAsync(customerId));

            Task.WaitAll(customerTask, orderTask);

            var customerResult = customerTask.Result;
            var orderResult = orderTask.Result;

            if (customerResult.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound($"Customer Id {customerId} was not found");
            }
            if (orderResult.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound($"No orders were found for customerId {customerId}");
            }
            var productIds = orderResult.ContentObject
                .SelectMany(o => o.Items.Select(oi => oi.ProductId))
                .ToList();

            var products = await _productsClient.GetProductsAsync(productIds);

            if (products.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound($"Products were not found for Customer Id {customerId}'s orders");
            }
            var customerOrders = new CustomerOrdersModel
            (
                customerResult.ContentObject,
                orderResult.ContentObject,
                products.ContentObject
            );
            return Ok(customerOrders);
        }
    }
}