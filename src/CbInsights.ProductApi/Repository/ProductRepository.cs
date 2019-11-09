using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CbInsights.Core;
using CbInsights.Domain;

namespace CbInsights.ProductsApi.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository()
        {
            _items = new List<Product>
            {
                new Product()
                {
                    Id = 0,
                    Name = "Pixel 3",
                    Price = 699.99M
                },
                new Product()
                {
                    Id = 1,
                    Name = "iPhone 7",
                    Price = 799.99M
                },
                new Product()
                {
                    Id = 2,
                    Name = "Surface 5",
                    Price = 1099.99M
                },
                new Product()
                {
                    Id = 3,
                    Name = "Lenovo Thinkpad",
                    Price = 1075.99M
                },
                new Product()
                {
                    Id = 4,
                    Name = "Darth Vader Action Figure",
                    Price = 10.99M
                }
            };
            _currentId = _items.Count;
        }
        public RepoResult<Product> DeleteProduct(int id)
        {
            return base.DeleteItem(id);
        }

        public RepoResult<Product> GetProductById(int id)
        {
            return base.GetItemById(id);
        }

        public RepoResult<List<Product>> GetProductsByIds(List<int> ids)
        {
            return base.GetItemsByIds(ids);
        }

        public RepoResult<Product> InsertProduct(Product product)
        {
            return base.InsertItem(product);
        }

        public RepoResult<Product> UpdateProduct(Product product)
        {
            return base.UpdateItem(product);
        }
    }
}
