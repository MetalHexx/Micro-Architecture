using CbInsights.ProductsApi.Controllers;
using CbInsights.ProductsApi.Models;
using CbInsights.ProductsApi.Repository;
using CbInsights.ProductsApi.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace CbInsights.ProductsApiTests
{
    public class ProductsApiTests
    {

        private Mock<IProductRepository> _blankRepositoryMock;
        private Mock<IPutValidator> _successPutValidatorMock;
        private Mock<IPostValidator> _successPostValidatorMock;
        private Mock<IPutValidator> _failedPutValidatorMock;
        private Mock<IPostValidator> _failedPostValidatorMock;

        public ProductsApiTests()
        {
            var successValidatonResult =
                new ValidationResult(
                    new List<ValidationFailure>());

            var failedValidationResult =
                new ValidationResult(
                    new List<ValidationFailure> {
                        new ValidationFailure("test", "test") });

            _successPutValidatorMock = new Mock<IPutValidator>();
            _successPutValidatorMock.Setup(v => v.Validate(It.IsAny<Product>())).Returns(successValidatonResult);
            _successPostValidatorMock = new Mock<IPostValidator>();
            _successPostValidatorMock.Setup(v => v.Validate(It.IsAny<Product>())).Returns(successValidatonResult);
            _failedPutValidatorMock = new Mock<IPutValidator>();
            _failedPutValidatorMock.Setup(v => v.Validate(It.IsAny<Product>())).Returns(failedValidationResult);
            _failedPostValidatorMock = new Mock<IPostValidator>();
            _failedPostValidatorMock.Setup(v => v.Validate(It.IsAny<Product>())).Returns(failedValidationResult);

            _blankRepositoryMock = new Mock<IProductRepository>();
        }

        [Theory]
        [ClassData(typeof(ProductListValidTestData))]
        public void WhenGetProductsCalled_WithValidIdList_ReturnsOkFoundProducts(List<Product> expectedProducts)
        {
            //Arrange
            var idsToFind = new List<int>() { 1, 2, 3};
            var repoResult = new RepoResult<List<Product>>(expectedProducts) { Type = RepoResultType.Success };
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.GetProductsByIds(It.IsAny<List<int>>())).Returns(repoResult);
            var controller = new ProductsController(
                repoMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var result = controller.GetProducts(idsToFind).Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(JsonConvert.SerializeObject(expectedProducts), JsonConvert.SerializeObject(result.Value));
        }

        [Fact]
        public void WhenGetProductsCalled_WithInvalidIdList_ReturnsNotFound()
        {
            var idsToFind = new List<int>() { 3, 4 };
            var repoResult = new RepoResult<List<Product>>(null) { Type = RepoResultType.NotFound };
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.GetProductsByIds(It.IsAny<List<int>>())).Returns(repoResult);
            var controller = new ProductsController(
                repoMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var result = controller.GetProducts(idsToFind);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Theory]
        [ClassData(typeof(ProductExistingTestData))]
        public void WhenGetProductCalled_WithValidProductId_ReturnOkProduct(Product expectedResult)
        {
            var repoResult = new RepoResult<Product>(expectedResult) { Type = RepoResultType.Success };
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.GetProductById(It.IsAny<int>())).Returns(repoResult);
            var controller = new ProductsController(
                repoMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var actualResult = controller.GetProduct(expectedResult.Id).Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(JsonConvert.SerializeObject(expectedResult), JsonConvert.SerializeObject(actualResult.Value));
        }

        [Theory]
        [ClassData(typeof(ProductNonExistentTestData))]
        public void WhenGetProductCalled_WithInvalidProductId_ReturnsNotFound(Product nonExistentProduct)
        {
            var repoResult = new RepoResult<Product>(nonExistentProduct) { Type = RepoResultType.NotFound };
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.GetProductById(It.IsAny<int>())).Returns(repoResult);
            var controller = new ProductsController(
                repoMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var actualResult = controller.GetProduct(nonExistentProduct.Id);

            //Assert
            Assert.IsType<NotFoundResult>(actualResult.Result);            
        }

        [Fact]
        public void WhenPostProductCalled_WithValidProduct_ReturnsOkIdResult()
        {
            var newProduct = new Product()
            {
                Id = 0,
                Name = "Test",
                Price = 1.00M
            };
            var savedProduct = new Product()
            {
                Id = 1,
                Name = "Test",
                Price = 1.00M
            };
            var repoResult = new RepoResult<Product>(savedProduct) { Type = RepoResultType.Success };
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.InsertProduct(newProduct)).Returns(repoResult);
            var controller = new ProductsController(
                repoMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);
            var expectedResult = new IdResult { Id = savedProduct.Id };

            //Act
            var actualResult = controller.PostProduct(newProduct).Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(JsonConvert.SerializeObject(expectedResult), JsonConvert.SerializeObject(actualResult.Value));
        }

        [Fact]
        public void WhenPostProductCalled_WithInvalidProduct_ReturnsBadRequest()
        {
            var controller = new ProductsController(
                _blankRepositoryMock.Object,
                _failedPutValidatorMock.Object,
                _failedPostValidatorMock.Object);

            //Act
            var result = controller.PostProduct(new Product()).Result;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result as ObjectResult);
        }

        [Fact]
        public void WhenPutProductCalled_WithValidProduct_ReturnsOkIdResult()
        {
            var updatedProduct = new Product()
            {
                Id = 1,
                Name = "Test",
                Price = 1.00M
            };
            var savedProduct = new Product()
            {
                Id = 1,
                Name = "Test",
                Price = 1.00M
            };
            var repoResult = new RepoResult<Product>(savedProduct) { Type = RepoResultType.Success };
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.UpdateProduct(updatedProduct)).Returns(repoResult);
            var controller = new ProductsController(
                repoMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var actualResult = controller.PutProduct(updatedProduct.Id, updatedProduct);

            //Assert
            Assert.IsType<OkResult>(actualResult);
        }

        [Theory]
        [ClassData(typeof(ProductExistingTestData))]
        public void WhenPutProductCalled_WithInvalidProduct_ReturnsBadRequest(Product existingProduct)
        {
            var controller = new ProductsController(
                _blankRepositoryMock.Object,
                _failedPutValidatorMock.Object,
                _failedPostValidatorMock.Object);

            //Act
            var result = controller.PutProduct(existingProduct.Id, existingProduct);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [ClassData(typeof(ProductNonExistentTestData))]
        public void WhenPutProductCalled_WithNonExistantProduct_ReturnsNotFound(Product nonExistentProduct)
        {
            var repoResult = new RepoResult<Product>(nonExistentProduct) { Type = RepoResultType.NotFound };
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.UpdateProduct(nonExistentProduct)).Returns(repoResult);
            var controller = new ProductsController(
                repoMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var actualResult = controller.PutProduct(nonExistentProduct.Id, nonExistentProduct);

            //Assert
            Assert.IsType<NotFoundResult>(actualResult);
        }

        [Theory]
        [ClassData(typeof(ProductExistingTestData))]
        public void WhenDeleteProductCalled_WithExistingProductId_ReturnsOk(Product existingProduct)
        {
            var repoResult = new RepoResult<Product>(existingProduct) { Type = RepoResultType.Success };
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.DeleteProduct(It.IsAny<int>())).Returns(repoResult);
            var controller = new ProductsController(
                repoMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var actualResult = controller.DeleteProduct(existingProduct.Id);

            //Assert
            Assert.IsType<OkResult>(actualResult);
        }

        [Theory]
        [ClassData(typeof(ProductNonExistentTestData))]
        public void WhenDeleteProductCalled_WithNonExistantProductId_ReturnsNotFound(Product nonExistingProduct)
        {
            var repoResult = new RepoResult<Product>(nonExistingProduct) { Type = RepoResultType.NotFound };
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.DeleteProduct(nonExistingProduct.Id)).Returns(repoResult);
            var controller = new ProductsController(
                repoMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var actualResult = controller.DeleteProduct(nonExistingProduct.Id);

            //Assert
            Assert.IsType<NotFoundResult>(actualResult);
        }
    }
}
