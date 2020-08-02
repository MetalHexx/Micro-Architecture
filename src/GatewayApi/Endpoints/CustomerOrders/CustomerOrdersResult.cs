using GatewayApi.Clients.Models;
using System.Collections.Generic;


namespace GatewayApi.Models
{
    public class CustomerOrdersResult
    {
        public Customer CustomerResult { get; set; }
        public List<Order> OrderResult { get; set; }
        public List<Product> ProductResult { get; set; }
    }
}
