﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GatewayApi.Clients;
using GatewayApi.Clients.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BaseGatewayController
    {
        private readonly CustomersClient _customerClient;

        public CustomersController(CustomersClient customerClient)
        {
            _customerClient = customerClient;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            try
            {
                var result = await _customerClient.GetCustomerAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }

        [HttpGet()]
        public async Task<ActionResult<ICollection<Customer>>> GetCustomers()
        {
            try
            {
                var result = await _customerClient.GetCustomersAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }

        [HttpPost()]
        public async Task<ActionResult<IdResult>> CreateCustomer([FromBody]Customer customer)
        {
            try
            {
                var result = await _customerClient.PostCustomerAsync(customer);
                return Ok(result);
            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IdResult>> UpdateCustomer(int id, [FromBody]Customer customer)
        {
            try
            {
                var result = await _customerClient.PostCustomerAsync(customer);
                return Ok(result);
            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _customerClient.DeleteCustomerAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }
    }
}