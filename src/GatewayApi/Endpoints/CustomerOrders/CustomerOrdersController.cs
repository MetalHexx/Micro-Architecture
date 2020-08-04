using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GatewayApi.Application.CustomerOrders.Queries;
using GatewayApi.Application.Customers.Queries;
using GatewayApi.Controllers;
using GatewayApi.Infrastructure.Clients.CustomersApi;
using GatewayApi.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GatewayApi.Infrastructure
{   
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrdersController : BaseGatewayController
    {
        private readonly ICustomersApiClient _customersClient;
        private readonly IMediator _mediator;

        public CustomerOrdersController(ICustomersApiClient customersClient, IMediator mediator)
        {
            _customersClient = customersClient;
            _mediator = mediator;
        }

        [HttpGet("customers/{customerId}/orders")]
        public async Task<ActionResult<CustomerOrdersViewModel>> GetCustomerOrders(int customerId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(new GetCustomerOrdersWithProducts(customerId), cancellationToken);

                var customerOrders = new CustomerOrdersViewModel
                (
                    result.Customer,
                    result.CustomerOrders,
                    result.OrderProducts
                );
                return Ok(customerOrders);
            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }            
        }

        [HttpGet("customers")]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetCustomerList()
        {
            try
            {
                var customers = await _mediator.Send(new GetAllCustomers());
                return Ok(customers.Select(c => new CustomerViewModel(c)));
            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }
    }
}