using CbInsights.Core;
using CbInsights.Domain;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CbInsights.Clients
{
    public class CustomersClient: ClientBase
    {
        public CustomersClient(HttpClient client, IOptions<ApiSettings> options) : base(client, options.Value.CustomersApiBaseUrl) { }

        public async Task<ApiResult<Customer>> GetCustomerByIdAsync(int id)
        {
            return await GetAsync<Customer>($"{id}");
        }

        public async Task<ApiResult<List<Customer>>> GetCustomersAsync()
        {
            return await GetAsync<List<Customer>>("");
        }

        public async Task<ApiResult<IdResult>> CreateCustomerAsync(Customer customer)
        {            
            return await PostAsync<IdResult>("", customer);
        }

        public async Task<ApiResult<string>> UpdateCustomerAsync(Customer customer)
        {
            return await PutAsync<string>($"{customer.Id}", customer);
        }

        public async Task<ApiResult<string>> DeleteCustomerAsync(int id)
        {
            return await DeleteAsync<string>($"{id}");
        }
    }
}
