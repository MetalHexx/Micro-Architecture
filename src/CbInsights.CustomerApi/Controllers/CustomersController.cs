using CbInsights.CustomerApi.Vaildators;
using CbInsights.CustomerApi.Validators;
using CbInsights.CustomersApi.Models;
using CbInsights.CustomersApi.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CbInsights.CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRespository _customersRepo;
        private readonly IPutValidator _putValidator;
        private readonly IPostValidator _postValidator;

        public CustomersController(
            ICustomersRespository customerRepo, 
            IPutValidator putValidator,
            IPostValidator postValidator)
        {
            _customersRepo = customerRepo;
            _putValidator = putValidator;
            _postValidator = postValidator;
        }
        [HttpGet()]
        public ActionResult<Customer> GetCustomers()
        {
            var result = _customersRepo.GetCustomers();

            if(result.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok(result.Entity);
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var result = _customersRepo.GetCustomer(id);

            if (result.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok(result.Entity);
        }

        [HttpPost]
        public ActionResult<IdResult> PostCustomer([FromBody] Customer customer)
        {
            var results = _postValidator.Validate(customer);
            results.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _customersRepo.InsertCustomer(customer);

            if (result.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok(new IdResult { Id = result.Entity.Id });
        }

        [HttpPut("{id}")]
        public ActionResult PutCustomer([FromRoute]int id, [FromBody] Customer customer)
        {
            var results = _putValidator.Validate(customer);
            results.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _customersRepo.UpdateCustomer(customer);

            if (result.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            var result = _customersRepo.DeleteCustomer(id);

            if (result.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}