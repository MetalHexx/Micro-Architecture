using CbInsights.GatewayApi.Clients.Models;
using System.Collections.Generic;

namespace CbInsights.GatewayApi.Models
{
    public class CustomerOrdersFlattenedModel
    {
        public Customer Customer { get; set; }
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
    }
}
