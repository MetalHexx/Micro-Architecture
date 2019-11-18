using CbInsights.GatewayApi.Clients.Models;
using System.Collections.Generic;
using System.Linq;

namespace CbInsights.GatewayApi.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public List<OrderItemModel> Items { get; set; }

        public OrderModel(Order order, List<Product> products = null)
        {
            OrderId = order.Id;
            OrderDate = order.OrderDate.ToString();
            Items = order.Items.Select(item => new OrderItemModel
            (
                products.First(p => p.Id == item.ProductId),
                item
            )).ToList();
        }
    }
}
