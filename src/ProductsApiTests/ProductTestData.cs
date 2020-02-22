using ProductsApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProductsApiTests
{
    public class ProductListValidTestData : TheoryData<List<Product>>
    {
        public ProductListValidTestData()
        {
            Add(new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "iPhone",
                    Price = 699.99M
                },
                new Product
                {
                    Id = 2,
                    Name = "Pixel 3",
                    Price = 599.99M
                }
            });
        }
    }

    public class ProductValidTestData : TheoryData<Product>
    {
        public ProductValidTestData()
        {
            Add(new Product
            {
                Id = 1,
                Name = "iPhone",
                Price = 699.99M
            });
        }
    }

    public class ProductInvalidTestData : TheoryData<Product>
    {
        public ProductInvalidTestData()
        {
            Add(new Product());

            Add(new Product
            {
                Id = 1,
                Price = 699.99M
            });

            Add(new Product
            {
                Id = 1,
                Name = "",
                Price = 699.99M
            });
        }
    }

    public class ProductNonExistentTestData : TheoryData<Product>
    {
        public ProductNonExistentTestData()
        {
            Add(new Product
            {
                Id = 0,
                Name = "iPhone",
                Price = 699.99M
            });
        }
    }

    public class ProductExistingTestData : TheoryData<Product>
    {
        public ProductExistingTestData()
        {
            Add(new Product
            {
                Id = 1,
                Name = "iPhone",
                Price = 699.99M
            });
        }
    }
}
