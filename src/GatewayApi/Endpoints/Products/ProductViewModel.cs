using GatewayApi.Domain.Clients.ProductsApi;

namespace GatewayApi.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public ProductViewModel(Product product)
        {
            Id = product.Id.Value;
            Name = product.Name;
            Price = product.Price.Value;
        }
    }
}
