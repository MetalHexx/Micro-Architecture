using System.Collections.Generic;
using CbInsights.OrdersApi.Models;
using CbInsights.OrdersApi.Repository;
using CbInsights.OrdersApi.Validators;
using FluentValidation.AspNetCore;
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
            var validator = new PostValidator();
            var result = validator.Validate(order);
            result.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            

            var repoResult = _ordersRepository.InsertOrder(order);

            if (repoResult.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok(new IdResult { Id = repoResult.Entity.Id });
        }

        [HttpPut("{id}")]
        public ActionResult PutOrder([FromRoute]int id, [FromBody] Order order)
        {
            var validator = new PutValidator();
            var result = validator.Validate(order);
            result.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var repoResult = _ordersRepository.UpdateOrder(order);

            if (repoResult.Type == RepoResultType.NotFound)
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