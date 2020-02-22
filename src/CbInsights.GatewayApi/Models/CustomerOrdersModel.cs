﻿using GatewayApi.Clients.Models;
using System.Collections.Generic;
using System.Linq;

namespace GatewayApi.Models
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
