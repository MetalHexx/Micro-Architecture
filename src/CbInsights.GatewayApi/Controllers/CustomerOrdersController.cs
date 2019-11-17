using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CbInsights.GatewayApi.Clients;
using CbInsights.GatewayApi.Clients.Models;
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
        [HttpGet("customers/{customerId}/orders")]
        public async Task<ActionResult<CustomerOrdersModel>> GetCustomerOrders(int customerId)
        {
            //Get the customer 
            var customerTask = Task.Run(
                () => _customersClient.GetCustomerByIdAsync(customerId));

            //Get the Customer Orders, wait, and get the order products
            var orderProductTask = Task.Run(async () =>
            {
                var ordersResult = await _ordersClient.GetCustomerOrdersAsync(customerId);
                ApiResult<List<Product>> productsResult = null;

                if (ordersResult.IsSuccess)
                {
                    var productIds = ordersResult.ContentObject
                        .SelectMany(o => o.Items.Select(oi => oi.ProductId))
                        .ToList();

                    productsResult = await _productsClient.GetProductsAsync(productIds);
                }
                return new { OrdersResult = ordersResult, ProductsResult = productsResult };
            });

            //wait for all api calls to complete
            await Task.WhenAll(customerTask, orderProductTask);

            var customerResult = customerTask.Result;
            var orderProductResult = orderProductTask.Result;

            //Validate that all calls were successful, if not return errors
            if (customerResult.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound($"Customer Id {customerId} was not found");
            }
            if (orderProductResult.OrdersResult.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound($"No orders were found for customerId {customerId}");
            }
            if (orderProductResult.ProductsResult.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound($"Products were not found for Customer Id {customerId}'s orders");
            }

            //Construct customer order viewmodel and return
            var customerOrders = new CustomerOrdersModel
            (
                customerResult.ContentObject,
                orderProductResult.OrdersResult.ContentObject,
                orderProductResult.ProductsResult.ContentObject
            );
            return Ok(customerOrders);
        }
    }
}