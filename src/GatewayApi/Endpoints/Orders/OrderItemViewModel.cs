using GatewayApi.Domain.Entities;

namespace GatewayApi.Models
{
    public class OrderItemViewModel
    {
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }

        public OrderItemViewModel(Product product, OrderItem orderItem)
        {
            Quantity = orderItem.Quantity.Value;
            Product = new ProductViewModel(product);
        }
    }
}
