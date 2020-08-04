using System.Collections.Generic;
using OrdersApi.Models;
using OrdersApi.Repository;
using OrdersApi.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace OrdersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IPutValidator _putValidator;
        private readonly IPostValidator _postValidator;
        private readonly IOrdersRepository _ordersRepository;

        public OrdersController(
            IOrdersRepository ordersRepository,
            IPutValidator putValidator,
            IPostValidator postValidator )
        {
            _ordersRepository = ordersRepository;
            _putValidator = putValidator;
            _postValidator = postValidator;
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
        public ActionResult<int> PostOrder([FromBody] Order order)
        {
            var result = _postValidator.Validate(order);
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
            return Ok(repoResult.Entity.Id);
        }

        [HttpPut("{id}")]
        public ActionResult PutOrder([FromRoute]int id, [FromBody] Order order)
        {
            var result = _putValidator.Validate(order);
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