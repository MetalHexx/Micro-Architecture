using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CbInsights.GatewayApi.Clients;
using CbInsights.GatewayApi.Clients.Models;
using CbInsights.GatewayApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CbInsights.GatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseGatewayController
    {
        private readonly OrdersClient _ordersClient;

        public OrdersController(OrdersClient ordersClient)
        {
            _ordersClient = ordersClient;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var result = await _ordersClient.GetOrderByIdAsync(id);
            return GetResult(result);
        }

        [HttpGet("~/api/customers/{customerId}/orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByCustomerId(int customerId)
        {
            var result = await _ordersClient.GetCustomerOrdersAsync(customerId);
            return GetResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<IdResult>> CreateOrder([FromBody] Order order)
        {
            var result = await _ordersClient.CreateOrderAsync(order);
            return GetResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder([FromRoute]int id, [FromBody] Order order)
        {
            var result = await _ordersClient.UpdateOrderAsync(order);
            return GetResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var result = await _ordersClient.DeleteOrderAsync(id);
            return GetResult(result);
        }
    }
}