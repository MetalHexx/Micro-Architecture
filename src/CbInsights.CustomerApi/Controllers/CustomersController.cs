﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CbInsights.Core;
using CbInsights.CustomerApi.Models;
using CbInsights.CustomerApi.Repository;
using CbInsights.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CbInsights.CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRespository _customerRepo;

        public CustomersController(ICustomerRespository customerRepo)
        {
            _customerRepo = customerRepo;
        }
        [HttpGet()]
        public async Task<ActionResult<Customer>> GetCustomers()
        {
            var result = _customerRepo.GetCustomers();

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

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var result = _customerRepo.GetCustomer(id);

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
        public async Task<ActionResult<IdResult>> CreateCustomer([FromBody] Customer customer)
        {
            var result = _customerRepo.InsertCustomer(customer);

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
        public async Task<ActionResult> UpdateCustomer([FromRoute]int id, [FromBody] Customer customer)
        {
            var result = _customerRepo.UpdateCustomer(customer);

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
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var result = _customerRepo.DeleteCustomer(id);
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