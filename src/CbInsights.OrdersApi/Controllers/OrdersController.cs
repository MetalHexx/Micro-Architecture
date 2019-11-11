using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult<Order> GetOrder(int id)
        {
            var result = _ordersRepository.GetOrderById(id);

            if (result.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok(result.Entity);
        }

        [HttpGet("~/api/customers/{customerId}/orders")]
        public ActionResult<IEnumerable<Order>> GetCustomerOrders(int customerId)
        {
            var result = _ordersRepository.GetOrdersByCustomerId(customerId);

            if (result.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok(result.Entity);
        }

        [HttpPost]
        public ActionResult<IdResult> PostOrder([FromBody] Order order)
        {
            var result = _ordersRepository.InsertOrder(order);

            if (result.Type == RepoResultType.NotFound)
            {
                return NotFound(new IdResult { Id = result.Entity.Id });
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult PutOrder([FromRoute]int id, [FromBody] Order order)
        {
            var result = _ordersRepository.UpdateOrder(order);

            if (result.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var result = _ordersRepository.DeleteOrder(id);

            if (result.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}