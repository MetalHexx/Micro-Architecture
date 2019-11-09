using CbInsights.Core;
using CbInsights.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
