using GatewayApi.Infrastructure.Clients.CustomersApi;
using GatewayApi.Infrastructure.Clients.OrdersApi;
using GatewayApi.Infrastructure.Clients.ProductsApi;
using System.Collections.Generic;
using System.Linq;

namespace GatewayApi.Models
{
    public class CustomerOrdersViewModel
    {
        public CustomerViewModel Customer { get; set; }
        public List<OrderViewModel> Orders { get; set; }

        public CustomerOrdersViewModel(Customer customer, List<Order> orders, List<Product>products)
        {
            Customer = new CustomerViewModel(customer);
            Orders = orders
                .Select(o => new OrderViewModel(o, products))
                .ToList();
        }
    }
}
