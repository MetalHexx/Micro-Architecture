using GatewayApi.Clients.Models;
using System.Collections.Generic;
using System.Linq;

namespace GatewayApi.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public List<OrderItemViewModel> Items { get; set; }

        public OrderViewModel(Order order, List<Product> products = null)
        {
            OrderId = order.Id;
            OrderDate = order.OrderDate.ToString();
            Items = order.Items.Select(item => new OrderItemViewModel
            (
                products.First(p => p.Id == item.ProductId),
                item
            )).ToList();
        }
    }
}
