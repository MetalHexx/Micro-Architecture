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
    public class ProductsController : BaseGatewayController
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
            return GetResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var result = await _productsClient.GetProductAsync(id);
            return GetResult(result);
        }

        [HttpPost()]
        public async Task<ActionResult<IdResult>> CreateProduct([FromBody, Required]Product product)
        {
            var result = await _productsClient.CreateProductAsync(product);
            return GetResult(result);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody, Required]Product product)
        {
            var result = await _productsClient.UpdateProductAsync(product);
            return GetResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var result = await _productsClient.DeleteProductAsync(id);
            return GetResult(result);
        }
    }
}