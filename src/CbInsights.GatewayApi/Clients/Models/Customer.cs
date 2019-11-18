using System;

namespace CbInsights.GatewayApi.Clients.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
