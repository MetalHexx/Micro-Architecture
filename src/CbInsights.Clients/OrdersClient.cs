using CbInsights.Core;
using CbInsights.Domain;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CbInsights.Clients
{
    public class OrdersClient: ClientBase
    {
        public OrdersClient(HttpClient client, IOptions<ApiSettings> options): base(client, options.Value.OrdersApiBaseUrl) { }

        public async Task<ApiResult<Order>> GetOrderByIdAsync(int orderId)
        {
            return await GetAsync<Order>($"orders/{orderId}");            
        }

        public async Task<ApiResult<Order>> GetOrderAsync(int id)
        {
            return await GetAsync<Order>($"{id}");
        }

        public async Task<ApiResult<List<Order>>> GetCustomerOrdersAsync(int customerId)
        {
            string path = $"customers/{customerId}/orders";
            return await GetAsync<List<Order>>(path);
        }

        public async Task<ApiResult<IdResult>> CreateOrderAsync(Order order)
        {
            string path = $"orders";
            return await PostAsync<IdResult>(path, order);
        }

        public async Task<ApiResult<string>> UpdateOrderAsync(Order order)
        {
            string path = $"orders/{order.Id.Value}";
            return await PutAsync<string>(path, order);
        }

        public async Task<ApiResult<string>> DeleteOrderAsync(int id)
        {
            return await DeleteAsync<string>($"orders/{id}");
        }
    }
}
