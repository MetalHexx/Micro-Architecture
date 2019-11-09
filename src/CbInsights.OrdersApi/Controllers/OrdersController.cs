using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CbInsights.Core;
using CbInsights.Domain;
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
            var result = _ordersRepository.GetOrderById(id);

            switch (result.Type)
            {
                case RepoResultType.NotFound:
                    return NotFound();
                case RepoResultType.Success:
                    return Ok(result.Entity);
                default:
                    return BadRequest();
            };
        }

        [HttpGet("~/api/customers/{customerId}/orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetCustomerOrders(int customerId)
        {
            var result = _ordersRepository.GetOrdersByCustomerId(customerId);

            switch (result.Type)
            {
                case RepoResultType.NotFound:
                    return NotFound();
                case RepoResultType.Success:
                    return Ok(result.Entity);
                default:
                    return BadRequest();
            };
        }

        [HttpPost]
        public async Task<ActionResult<IdResult>> CreateOrder([FromBody] Order order)
        {
            var result = _ordersRepository.InsertOrder(order);
            
            switch (result.Type)
            {
                case RepoResultType.NotFound:
                    return NotFound();
                case RepoResultType.Success:
                    return Ok(new IdResult { Id = result.Entity.Id.Value });
                default:
                    return BadRequest();
            };
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder([FromRoute]int id, [FromBody] Order order)
        {
            var result = _ordersRepository.UpdateOrder(order);

            switch (result.Type)
            {
                case RepoResultType.NotFound:
                    return NotFound();
                case RepoResultType.Success:
                    return Ok();
                default:
                    return BadRequest();
            };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var result = _ordersRepository.DeleteOrder(id);

            switch (result.Type)
            {
                case RepoResultType.NotFound:
                    return NotFound();
                case RepoResultType.Success:
                    return Ok();
                default:
                    return BadRequest();
            };
        }
    }
}