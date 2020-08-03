using GatewayApi.Domain.Clients.CustomersApi;
using GatewayApi.Domain.Clients.OrdersApi;
using GatewayApi.Domain.Clients.ProductsApi;
using GatewayApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayApi.Controllers.CustomerOrders
{
    public interface ICustomerOrdersService
    {
        Task<CustomerOrdersResult> GetCustomerOrdersAsync(int customerId);
    }

    /// <summary>
    /// Service dedicated to the customer orders view and used to fetch customer order
    /// data from downstream apis
    /// </summary>
    public class CustomerOrdersService : ICustomerOrdersService
    {
        private readonly CustomersApiClient _customersClient;
        private readonly OrdersApiClient _ordersClient;
        private readonly ProductsApiClient _productsClient;

        public CustomerOrdersService(CustomersApiClient customersClient, OrdersApiClient ordersClient, ProductsApiClient productsClient)
        {
            _customersClient = customersClient;
            _ordersClient = ordersClient;
            _productsClient = productsClient;
        }
        public async Task<CustomerOrdersResult> GetCustomerOrdersAsync(int customerId)
        {
            //Get the customer 
            var customerTask = Task.Run(
                () => _customersClient.GetCustomerAsync(customerId));

            //Get the Customer Orders, wait, and get the order products
            var orderProductTask = Task.Run(async () =>
            {
                var ordersResult = await _ordersClient.GetCustomerOrdersAsync(customerId);

                var productIds = ordersResult
                    .SelectMany(o => o.Items.Select(oi => oi.ProductId.Value))
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
