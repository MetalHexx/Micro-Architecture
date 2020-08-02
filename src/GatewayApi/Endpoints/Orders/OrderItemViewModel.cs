
using GatewayApi.Clients.Models;

namespace GatewayApi.Models
{
    public class OrderItemViewModel
    {
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }

        public OrderItemViewModel(Product product, OrderItem orderItem)
        {
            Quantity = orderItem.Quantity;
            Product = new ProductViewModel(product);
        }
    }
}
