using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CbInsights.Domain;
using CbInsights.OrdersApi.Models;
using CbInsights.OrdersApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CbInsights.OrdersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersController(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = _ordersRepository.GetOrderById(id);

            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("~/api/customers/{customerId}/orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetCustomerOrders(int customerId)
        {
            var orders = _ordersRepository.GetOrdersByCustomerId(customerId).ToList();

            if (orders == null || orders.Count == 0)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<IdResult>> CreateOrder([FromBody] Order order)
        {
            var id = _ordersRepository.InsertOrder(order);
            return Ok(new IdResult { Id = id });
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