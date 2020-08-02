
using GatewayApi.Clients.Models;

namespace GatewayApi.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ProductViewModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
        }
    }
}
