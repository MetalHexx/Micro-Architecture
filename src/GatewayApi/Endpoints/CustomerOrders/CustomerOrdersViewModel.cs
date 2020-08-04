using GatewayApi.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GatewayApi.Models
{
    public class CustomerOrdersViewModel
    {
        public CustomerViewModel Customer { get; set; }
        public IEnumerable<OrderViewModel> Orders { get; set; }

        public CustomerOrdersViewModel(Customer customer, IEnumerable<Order> orders, IEnumerable<Product>products)
        {
            Customer = new CustomerViewModel(customer);
            Orders = orders
                .Select(o => new OrderViewModel(o, products));
        }
    }
}
