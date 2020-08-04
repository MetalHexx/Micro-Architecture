using AutoMapper;
using GatewayApi.Domain.Entities;
using GatewayApi.Infrastructure.Clients.CustomersApi;
using GatewayApi.Infrastructure.Clients.OrdersApi;
using GatewayApi.Infrastructure.Clients.ProductsApi;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApiOrder = GatewayApi.Infrastructure.Clients.OrdersApi.Order;
using ApiProduct = GatewayApi.Infrastructure.Clients.ProductsApi.Product;
using Customer = GatewayApi.Domain.Entities.Customer;
using ApiCustomer = GatewayApi.Infrastructure.Clients.CustomersApi.Customer;
using Order = GatewayApi.Domain.Entities.Order;
using Product = GatewayApi.Domain.Entities.Product;

namespace GatewayApi.Application.CustomerOrders.Queries
{
    public class GetCustomerOrdersWithProducts: IRequest<CustomerOrdersWithProductsResponse> 
    {
        public GetCustomerOrdersWithProducts(int customerId)
        {
            CustomerId = customerId;
        }
        public int CustomerId { get; }
    }

    public class CustomerOrdersWithProductsResponse
    {
        public Customer CustomerResult { get; set; }
        public List<Order> OrderResult { get; set; }
        public List<Product> ProductResult { get; set; }
    }

    public class GetCustomerOrdersProductsHandler : IRequestHandler<GetCustomerOrdersWithProducts, CustomerOrdersWithProductsResponse>
    {
        private readonly ICustomersApiClient _customersClient;
        private readonly IOrdersApiClient _ordersClient;
        private readonly IProductsApiClient _productsClient;
        private readonly IMapper _mapper;

        public GetCustomerOrdersProductsHandler(ICustomersApiClient customersClient, IOrdersApiClient ordersClient, IProductsApiClient productsClient, IMapper mapper)
        {
            _customersClient = customersClient;
            _ordersClient = ordersClient;
            _productsClient = productsClient;
            _mapper = mapper;
        }

        public async Task<CustomerOrdersWithProductsResponse> Handle(GetCustomerOrdersWithProducts request, CancellationToken cancellationToken)
        {
            //Get the customer 
            var customerTask = Task.Run(
                () => _customersClient.GetCustomerAsync(request.CustomerId, cancellationToken));

            //Get the Customer Orders, wait, and get the order products
            var orderProductTask = Task.Run(async () =>
            {
                var ordersResult = await _ordersClient.GetCustomerOrdersAsync(request.CustomerId, cancellationToken);

                var productIds = ordersResult
                    .SelectMany(o => o.Items.Select(oi => oi.ProductId.Value))
                    .ToList();

                var productsResult = await _productsClient.GetProductsAsync(productIds, cancellationToken);
                return new { OrdersResult = ordersResult, ProductsResult = productsResult };
            });

            //wait for all api calls to complete
            await Task.WhenAll(customerTask, orderProductTask);

            var customerResult = customerTask.Result;
            var orderProductResult = orderProductTask.Result;

            return new CustomerOrdersWithProductsResponse
            {
                CustomerResult = _mapper.Map<ApiCustomer, Customer>(customerResult),

                OrderResult = orderProductResult.OrdersResult
                    .Select(o => _mapper.Map<ApiOrder, Order>(o))
                    .ToList(),
                
                ProductResult = orderProductResult.ProductsResult
                    .Select(p => _mapper.Map<ApiProduct, Product>(p))
                .ToList()
            };
        }
    }
}
