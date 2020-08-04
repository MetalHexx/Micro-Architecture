using CustomersApi.Application.Commands;
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
using System.Threading;
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
        public async Task<ActionResult<List<Customer>>> GetCustomers(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllCustomers(), cancellationToken);

            if(result.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok(result.Entity);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCustomerById(id), cancellationToken);

            if (result.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok(result.Entity);
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostCustomer([FromBody] Customer customer, CancellationToken cancellationToken)
        {
            var results = _postValidator.Validate(customer);
            results.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mediator.Send(new CreateCustomer(customer));

            if (result.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok(result.Entity.Id);
        }

        [HttpPut("{id}")]
        public ActionResult PutCustomer([FromRoute]int id, [FromBody] Customer customer, CancellationToken cancellationToken)
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
        public ActionResult DeleteCustomer(int id, CancellationToken cancellationToken)
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