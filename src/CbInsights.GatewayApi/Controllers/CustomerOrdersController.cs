using System;
using System.Linq;
using System.Threading.Tasks;
using CbInsights.GatewayApi.Clients;
using CbInsights.GatewayApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CbInsights.GatewayApi.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrdersController : BaseGatewayController
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
            try
            {
                var result = await GetCustomerOrdersAsync(customerId);
                var customerOrders = new CustomerOrdersModel
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

        private async Task<CustomerOrdersResult> GetCustomerOrdersAsync(int customerId)
        {   
            //Get the customer 
            var customerTask = Task.Run(
                () => _customersClient.GetCustomerAsync(customerId));

            //Get the Customer Orders, wait, and get the order products
            var orderProductTask = Task.Run(async () =>
            {
                var ordersResult = await _ordersClient.GetCustomerOrdersAsync(customerId);
                var productIds = ordersResult
                    .SelectMany(o => o.Items.Select(oi => oi.ProductId))
                    .ToList();

                var productsResult = await _productsClient.GetProductsAsync(productIds);
                return new { OrdersResult = ordersResult, ProductsResult = productsResult };
            });

            //wait for all api calls to complete
            await Task.WhenAll(customerTask, orderProductTask);

            var customerResult = customerTask.Result;
            var orderProductResult = orderProductTask.Result;

            return new CustomerOrdersResult
            {
                CustomerResult = customerResult,
                OrderResult = orderProductResult.OrdersResult.ToList(),
                ProductResult = orderProductResult.ProductsResult.ToList()
            };
        }
    }
}