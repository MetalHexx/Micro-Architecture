using CbInsights.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.GatewayApi.Models
{
    public class CustomerOrdersModel
    {
        public CustomerModel Customer { get; set; }
        public List<OrderModel> Orders { get; set; }

        public CustomerOrdersModel(Customer customer, List<Order> orders, List<Product>products)
        {
            Customer = new CustomerModel(customer);
            Orders = orders
                .Select(o => new OrderModel(o, products))
                .ToList();
        }
    }
}
