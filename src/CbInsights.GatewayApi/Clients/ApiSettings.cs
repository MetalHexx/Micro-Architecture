using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayApi.Clients
{
    public class ApiSettings
    {
        public string CustomersApiBaseUrl { get; set; }
        public string OrdersApiBaseUrl { get; set; }
        public string ProductsApiBaseUrl { get; set; }
    }
}
