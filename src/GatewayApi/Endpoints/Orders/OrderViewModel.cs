﻿using GatewayApi.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GatewayApi.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public IEnumerable<OrderItemViewModel> Items { get; set; }

        public OrderViewModel(Order order, IEnumerable<Product> products = null)
        {
            OrderId = order.Id.Value;
            OrderDate = order.OrderDate.ToString();
            Items = order.Items.Select(item => new OrderItemViewModel
            (
                products.First(p => p.Id == item.ProductId),
                item
            )).ToList();
        }
    }
}
