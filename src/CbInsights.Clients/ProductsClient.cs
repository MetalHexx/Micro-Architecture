using CbInsights.Core;
using CbInsights.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CbInsights.Clients
{
    public class ProductsClient: ClientBase
    {
        public ProductsClient(HttpClient client) : base(client, "http://localhost:5003/api/products/") { }

        public async Task<ApiResult<List<Product>>> GetProductsAsync(List<int> ids)
        {
            var query = "?";

            foreach (var id in ids)
            {
                query += $"ids={id}&";
            }
            return await GetAsync<List<Product>>($"{query.Substring(0, query.Length - 1)}");
        }

        public async Task<ApiResult<Product>> GetProductAsync(int id)
        {
            return await GetAsync<Product>($"{id}");
        }

        public async Task<ApiResult<IdResult>> CreateProductAsync(Product product)
        {
            return await PostAsync<IdResult>("", product);
        }

        public async Task<ApiResult<string>> UpdateProductAsync(Product product)
        {
            string path = $"{product.Id}";
            return await PutAsync<string>(path, product);
        }

        public async Task<ApiResult<string>> DeleteProductAsync(int id)
        {
            return await DeleteAsync<string>($"{id}");
        }
    }
}
