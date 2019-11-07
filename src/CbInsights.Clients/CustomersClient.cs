using CbInsights.Domain;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CbInsights.Clients
{
    public class CustomersClient: ClientBase
    {
        public CustomersClient(HttpClient client) : base(client, "http://localhost:5001/api/customers/") { }

        public async Task<ApiResult<Customer>> GetCustomerByIdAsync(int id)
        {
            return await GetAsync<Customer>($"{id}");
        }
    }
}
