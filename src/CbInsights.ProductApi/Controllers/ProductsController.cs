using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CbInsights.Core;
using CbInsights.Domain;
using CbInsights.ProductsApi.Models;
using CbInsights.ProductsApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace CbInsights.ProductsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductsController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByIds([FromQuery(Name = "ids")] List<int> ids)
        {
            var result = _productRepo.GetProductsByIds(ids);
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
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var result = _productRepo.GetProductById(id);

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

        [HttpPost()]
        public async Task<ActionResult<IdResult>> CreateProduct([FromBody, Required]Product product)
        {           
            var result = _productRepo.UpdateProduct(product);

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
        public async Task<ActionResult> UpdateProduct(int id, [FromBody, Required]Product product)
        {
            var result = _productRepo.UpdateProduct(product);

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
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var result = _productRepo.DeleteProduct(id);

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