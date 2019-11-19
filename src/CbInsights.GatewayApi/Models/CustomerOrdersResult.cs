using CbInsights.GatewayApi.Clients.Models;
using System.Collections.Generic;


namespace CbInsights.GatewayApi.Models
{
    public class CustomerOrdersResult
    {
        public Customer CustomerResult { get; set; }
        public List<Order> OrderResult { get; set; }
        public List<Product> ProductResult { get; set; }
    }
}
