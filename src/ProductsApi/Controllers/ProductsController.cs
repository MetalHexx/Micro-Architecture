using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProductsApi.Models;
using ProductsApi.Repository;
using ProductsApi.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;

namespace ProductsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly IPutValidator _putValidator;
        private readonly IPostValidator _postValidator;

        public ProductsController(IProductRepository productRepo, IPutValidator putValidator, IPostValidator postValidator)
        {
            _productRepo = productRepo;
            _putValidator = putValidator;
            _postValidator = postValidator;
        }
        [HttpGet()]
        public ActionResult<List<Product>> GetProducts([FromQuery(Name = "ids")] List<int> ids)
        {
            var result = _productRepo.GetProductsByIds(ids);

            if (result.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok(result.Entity);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var result = _productRepo.GetProductById(id);

            if (result.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok(result.Entity);
        }

        [HttpPost()]
        public ActionResult<IdResult> PostProduct([FromBody, Required]Product product)
        {
            var results = _postValidator.Validate(product);
            results.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _productRepo.InsertProduct(product);

            if (result.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok(new IdResult { Id = result.Entity.Id });
        }



        [HttpPut("{id}")]
        public ActionResult PutProduct(int id, [FromBody, Required]Product product)
        {
            var results = _putValidator.Validate(product);
            results.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _productRepo.UpdateProduct(product);

            if (result.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var result = _productRepo.DeleteProduct(id);

            if (result.Type == RepoResultType.NotFound)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}