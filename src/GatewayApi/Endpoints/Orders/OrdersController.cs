using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GatewayApi.Application.Common.Exceptions;
using GatewayApi.Domain.Clients.OrdersApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseGatewayController
    {
        private readonly OrdersApiClient _ordersClient;

        public OrdersController(OrdersApiClient ordersClient)
        {
            _ordersClient = ordersClient;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            try
            {
                var result = await _ordersClient.GetOrderAsync(id);
                return Ok(result);

            }
            catch (ApiException e)
            {
                return GenerateErrorResult(e);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("~/api/customers/{customerId}/orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByCustomerId(int customerId)
        {
            try
            {
                var result = await _ordersClient.GetCustomerOrdersAsync(customerId);
                return Ok(result);

            }
            catch (ApiException e)
            {
                return GenerateErrorResult(e);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateOrder([FromBody] Order order)
        {
            try
            {
                var result = await _ordersClient.PostOrderAsync(order);
                return Ok(result);

            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder([FromRoute]int id, [FromBody] Order order)
        {
            try
            {
                await _ordersClient.PutOrderAsync(id, order);
                return Ok();

            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            try
            {
                await _ordersClient.DeleteOrderAsync(id);
                return Ok();

            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }
    }
}