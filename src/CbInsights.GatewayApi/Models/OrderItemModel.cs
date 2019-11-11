using CbInsights.GatewayApi.Clients.Models;

namespace CbInsights.GatewayApi.Models
{
    public class OrderItemModel
    {
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }

        public OrderItemModel(Product product, OrderItem orderItem)
        {
            Quantity = orderItem.Quantity;
            Product = new ProductModel(product);
        }
    }
}
