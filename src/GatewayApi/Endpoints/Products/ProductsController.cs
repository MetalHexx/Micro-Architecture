using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using GatewayApi.Infrastructure.Clients.ProductsApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace GatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseGatewayController
    {
        private readonly IProductsApiClient _productsClient;

        public ProductsController(IProductsApiClient productsClient)
        {
            _productsClient = productsClient;
        }
        [HttpGet()]
        public async Task<ActionResult<List<Product>>> GetProductsByIds([FromQuery(Name = "ids")] List<int> ids)
        {
            try
            {
                var result = await _productsClient.GetProductsAsync(ids);
                return Ok(result);

            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                var result = await _productsClient.GetProductAsync(id);
                return Ok(result);

            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }

        [HttpPost()]
        public async Task<ActionResult<int>> CreateProduct([FromBody, Required]Product product)
        {
            try
            {
                var result = await _productsClient.PostProductAsync(product);
                return Ok(result);

            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody, Required]Product product)
        {
            try
            {
                await _productsClient.PutProductAsync(id, product);
                return Ok();
            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productsClient.DeleteProductAsync(id);
                return Ok();

            }
            catch (Exception e)
            {
                return GenerateErrorResult(e);
            }
        }
    }
}