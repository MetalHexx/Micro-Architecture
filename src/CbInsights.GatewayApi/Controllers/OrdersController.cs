using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CbInsights.Clients;
using CbInsights.Core;
using CbInsights.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CbInsights.GatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
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

            if (result.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }
            return Ok(result.Content);
        }

        [HttpGet("~/api/customers/{customerId}/orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetCustomerOrders(int customerId)
        {
            var result = await _ordersClient.GetCustomerOrdersAsync(customerId);

            if (result.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }
            return Ok(result.Content);
        }

        [HttpPost]
        public async Task<ActionResult<IdResult>> CreateOrder([FromBody] Order order)
        {
            var result = await _ordersClient.CreateOrderAsync(order);
            return Ok(new IdResult { Id = result.ContentObject.Id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder([FromRoute]int id, [FromBody] Order order)
        {
            var result = await _ordersClient.UpdateOrderAsync(order);

            if (result.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var result = await _ordersClient.DeleteOrderAsync(id);

            if (result.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}