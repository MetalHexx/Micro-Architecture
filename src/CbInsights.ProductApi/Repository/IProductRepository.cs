using CbInsights.ProductsApi.Models;
using System.Collections.Generic;

namespace CbInsights.ProductsApi.Repository
{
    public interface IProductRepository
    {
        RepoResult<Product> GetProductById(int id);
        RepoResult<List<Product>> GetProductsByIds(List<int> ids);
        RepoResult<Product> InsertProduct(Product product);
        RepoResult<Product> UpdateProduct(Product product);
        RepoResult<Product> DeleteProduct(int id);
    }
}
