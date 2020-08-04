using CustomersApi.Application.Queries;
using CustomersApi.Application.Validations;
using CustomersApi.Domain;
using CustomersApi.Infrastructure.Persistance;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository _customersRepo;
        private readonly IMediator _mediator;
        private readonly IPutValidator _putValidator;
        private readonly IPostValidator _postValidator;

        public CustomersController(
            ICustomersRepository customersRepo,
            IMediator mediator,
            IPutValidator putValidator,
            IPostValidator postValidator)
        {
            _customersRepo = customersRepo;
            _mediator = mediator;
            _putValidator = putValidator;
            _postValidator = postValidator;
        }
        [HttpGet()]
        public async Task<ActionResult<List<Customer>>> GetCustomers()
        {
            var result = await _mediator.Send(new GetAllCustomers());

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
        public ActionResult<int> PostCustomer([FromBody] Customer customer)
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
            return Ok(result.Entity.Id);
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