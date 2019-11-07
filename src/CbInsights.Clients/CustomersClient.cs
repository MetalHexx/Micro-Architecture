using CbInsights.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public async Task<ApiResult<List<Customer>>> GetCustomersAsync()
        {
            return await GetAsync<List<Customer>>("");
        }

        public async Task<ApiResult<int>> CreateCustomerAsync(Customer customer)
        {            
            return await PostAsync<int>("", customer);
        }

        public async Task<ApiResult<int>> UpdateCustomerAsync(Customer customer)
        {
            return await PutAsync<int>($"{customer.Id}", customer);
        }

        public async Task<ApiResult<string>> DeleteCustomerAsync(int id)
        {
            return await DeleteAsync<string>($"{id}");
        }
    }
}
