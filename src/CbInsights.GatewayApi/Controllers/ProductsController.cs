using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CbInsights.GatewayApi.Clients;
using CbInsights.GatewayApi.Clients.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CbInsights.GatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsClient _productsClient;

        public ProductsController(ProductsClient productsClient)
        {
            _productsClient = productsClient;
        }
        [HttpGet()]
        public async Task<ActionResult<List<Product>>> GetProductsByIds([FromQuery(Name = "ids")] List<int> ids)
        {
            var result = await _productsClient.GetProductsAsync(ids);

            if (result.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }
            return Ok(result.Content);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var result = await _productsClient.GetProductAsync(id);

            if (result.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }
            return Ok(result.Content);
        }

        [HttpPost()]
        public async Task<ActionResult<IdResult>> CreateProduct([FromBody, Required]Product product)
        {
            var result = await _productsClient.CreateProductAsync(product);
            return Ok(new IdResult {Id = result.ContentObject.Id });
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody, Required]Product product)
        {
            var result = await _productsClient.UpdateProductAsync(product);

            if (result.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var result = await _productsClient.DeleteProductAsync(id);

            if (result.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}