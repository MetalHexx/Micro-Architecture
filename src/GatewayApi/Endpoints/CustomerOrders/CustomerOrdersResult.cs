using GatewayApi.Infrastructure.Clients.CustomersApi;
using GatewayApi.Infrastructure.Clients.OrdersApi;
using GatewayApi.Infrastructure.Clients.ProductsApi;
using System.Collections.Generic;


namespace GatewayApi.Models
{
    public class CustomerOrdersResult
    {
        public Customer CustomerResult { get; set; }
        public List<Order> OrderResult { get; set; }
        public List<Product> ProductResult { get; set; }
    }
}
