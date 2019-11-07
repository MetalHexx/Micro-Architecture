using CbInsights.Domain;
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
        public OrdersClient(HttpClient client): base(client, "http://localhost:5000/api/") { }

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
            string path = $"api//customers//{customerId}// orders";
            return await GetAsync<List<Order>>(path);
        }

        public async Task<ApiResult<int>> CreateOrderAsync(Order order)
        {
            string path = $"api//customers//{order.CustomerId}// orders";
            return await PostAsync<int>(path, order);
        }

        public async Task<ApiResult<string>> UpdateOrderAsync(Order order)
        {
            string path = $"api//customers//{order.CustomerId}// orders";
            return await PutAsync<string>(path, order);
        }

        public async Task<ApiResult<string>> DeleteOrderAsync(int id)
        {
            return await DeleteAsync<string>($"{id}");
        }
    }
}
