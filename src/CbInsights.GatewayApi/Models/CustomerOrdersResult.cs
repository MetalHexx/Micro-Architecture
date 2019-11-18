using CbInsights.GatewayApi.Clients;
using CbInsights.GatewayApi.Clients.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.GatewayApi.Models
{
    public class CustomerOrdersResult
    {
        public ApiResult<Customer> CustomerResult { get; set; }
        public ApiResult<List<Order>> OrderResult { get; set; }
        public ApiResult<List<Product>> ProductResult { get; set; }
    }
}
