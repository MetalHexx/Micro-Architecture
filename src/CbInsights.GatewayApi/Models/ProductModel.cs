
using GatewayApi.Clients.Models;

namespace GatewayApi.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ProductModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
        }
    }
}
