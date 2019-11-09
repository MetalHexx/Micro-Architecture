using CbInsights.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.GatewayApi.Models
{
    public class OrderModel
    {
        public int? OrderId { get; set; }
        public List<OrderItemModel> Items { get; set; }

        public OrderModel(Order order, List<Product> products)
        {
            OrderId = order.Id;
            Items = order.Items.Select(item => new OrderItemModel
            (
                products.First(p => p.Id == item.ProductId),
                item
            )).ToList();
        }
    }
}
