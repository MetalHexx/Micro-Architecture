﻿using CbInsights.Domain;
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
            return await SendRequest<Order>($"orders/{orderId}", HttpMethod.Get);            
        }

        public async Task<ApiResult<Order>> GetOrderAsync(int id)
        {
            return await SendRequest<Order>($"{id}", HttpMethod.Get);
        }

        public async Task<ApiResult<List<Order>>> GetCustomerOrdersAsync(int customerId)
        {
            return await SendRequest<List<Order>>(
                $"api//customers//{customerId}// orders", 
                HttpMethod.Get);
        }

        public async Task<ApiResult<int>> CreateOrder(Order order)
        {
            var id = _ordersRepository.InsertOrder(order);
            return Ok(new OrderCreateResult { Id = id });
        }

        [HttpPut("{id}")]
        public async Task UpdateOrder([FromRoute]int id, [FromBody] Order order)
        {
            _ordersRepository.UpdateOrder(order);
        }

        [HttpDelete("{id}")]
        public async Task DeleteOrder(int id)
        {
            _ordersRepository.DeleteOrder(id);
        }
    }
}
