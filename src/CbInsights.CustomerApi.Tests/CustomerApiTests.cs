using CbInsights.CustomerApi.Controllers;
using CbInsights.CustomersApi.Models;
using CbInsights.CustomersApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CbInsights.CustomerApi.Tests
{
    public class CustomerApiTests
    {
        private IEnumerable<Customer> _customerListTestData;
        private Customer _customerTestData;
        private string _customerListTestDataJson;
        private string _customerTestDataJson;        

        public CustomerApiTests()
        {
            _customerListTestData = new List<Customer>()
            {
                new Customer
                {
                    Id = 0,
                    FirstName = "William",
                    LastName = "Pereira"
                },
                new Customer
                {
                    Id = 1,
                    FirstName = "Luke",
                    LastName = "Skywalker"
                }
            };
            _customerListTestDataJson = JsonConvert.SerializeObject(_customerListTestData);
            _customerTestData = _customerListTestData.First();
            _customerTestDataJson = JsonConvert.SerializeObject(_customerTestData);
        }

        [Fact]
        public void WhenGetCustomersCalled_ReturnsOkCustomerList()
        {
            //Arrange     
            var expectedContent = _customerListTestDataJson;
            var repoResult = new RepoResult<IEnumerable<Customer>>(_customerListTestData) { Type = RepoResultType.Success };
            
            var customerRepoMock = new Mock<ICustomersRespository>();
            customerRepoMock.Setup(repo => repo.GetCustomers()).Returns(repoResult);
            var customersController = new CustomersController(customerRepoMock.Object);

            //Act
            var actualResult = customersController.GetCustomers().Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(expectedContent, JsonConvert.SerializeObject(actualResult.Value));
        }

        [Fact]
        public void WhenGetCustomerCalled_AndCustomerExists_ReturnsOkCustomer()
        {
            //Arrange     
            var expectedContent = _customerTestDataJson;
            var repoResult = new RepoResult<Customer>(_customerTestData) { Type = RepoResultType.Success };

            var customerRepoMock = new Mock<ICustomersRespository>();
            customerRepoMock.Setup(repo => repo.GetCustomer(0)).Returns(repoResult);
            var customersController = new CustomersController(customerRepoMock.Object);

            //Act
            var actualResult = customersController.GetCustomer(0).Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(expectedContent, JsonConvert.SerializeObject(actualResult.Value));
        }

        [Fact]
        public void WhenGetCustomerCalled_ReturnsNotFound()
        {
            //Arrange     
            var repoResult = new RepoResult<Customer>(null) { Type = RepoResultType.NotFound };
            var customerRepoMock = new Mock<ICustomersRespository>();
            customerRepoMock.Setup(repo => repo.GetCustomer(0)).Returns(repoResult);
            var customersController = new CustomersController(customerRepoMock.Object);

            //Act
            var actualResult = customersController.GetCustomer(0).Result;

            //Assert
            Assert.IsType<NotFoundResult>(actualResult);
        }

        [Theory]
        [InlineData("FirstName", "LastName", 0)]
        public void WhenPostCustomerCalled_WithValidCustomer_ReturnsOkResultIdResult(string firstName, string lastName, int id)
        {
            //Arrange
            var newCustomer = new Customer()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };
            var repoCustomer = new Customer()
            {
                Id = 1,
                FirstName = newCustomer.FirstName,
                LastName = newCustomer.LastName
            };
            var expectedIdResult = JsonConvert.SerializeObject(new IdResult { Id = repoCustomer.Id });           
            var repoResult = new RepoResult<Customer>(repoCustomer) { Type = RepoResultType.Success };
            var customerRepoMock = new Mock<ICustomersRespository>();
            customerRepoMock.Setup(repo => repo.InsertCustomer(It.IsAny<Customer>())).Returns(repoResult);
            var customersController = new CustomersController(customerRepoMock.Object);

            //Act
            var actualResult = customersController.PostCustomer(newCustomer).Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(expectedIdResult, JsonConvert.SerializeObject(actualResult.Value));
        }

        [Theory]
        [InlineData("FirstName", "LastName", 1)]
        [InlineData("", "", 0)]
        public void WhenPostCustomerCalled_WithInvalidCustomer_ReturnsBadRequest(string firstName, string lastName, int id)
        {
            //Arrange
            var updatedCustomer = new Customer()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };           
            
            var customerRepoMock = new Mock<ICustomersRespository>();            
            var customersController = new CustomersController(customerRepoMock.Object);

            //Act
            var result = customersController.PostCustomer(updatedCustomer).Result as ActionResult;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData("FirstName", "LastName", 1)]
        public void WhenPutCustomerCalled_WithValidCustomer_ReturnsOk(string firstName, string lastName, int id)
        {
            //Arrange
            var updatedCustomer = new Customer()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };
            var repoResult = new RepoResult<Customer>(updatedCustomer) { Type = RepoResultType.Success };
            var customerRepoMock = new Mock<ICustomersRespository>();
            customerRepoMock.Setup(repo => repo.UpdateCustomer(It.IsAny<Customer>())).Returns(repoResult);
            var customersController = new CustomersController(customerRepoMock.Object);

            //Act
            var actualResult = customersController.PutCustomer(updatedCustomer.Id, updatedCustomer);

            //Assert
            Assert.IsType<OkResult>(actualResult);
        }

        [Theory]
        [InlineData("FirstName", "LastName", 0)]
        [InlineData("", "", 1)]
        public void WhenPutCustomerCalled_WithInvalidCustomer_ReturnsBadRequest(string firstName, string lastName, int id)
        {
            //Arrange
            var updatedCustomer = new Customer()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };

            var customerRepoMock = new Mock<ICustomersRespository>();
            var customersController = new CustomersController(customerRepoMock.Object);

            //Act
            var result = customersController.PutCustomer(updatedCustomer.Id, updatedCustomer) as ActionResult;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Theory]
        [InlineData("FirstName", "LastName", 6)]
        public void WhenPutCustomerCalled_WithNonExistantCustomer_ReturnsNotFound(string firstName, string lastName, int id)
        {
            //Arrange
            var updatedCustomer = new Customer()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };
            var repoResult = new RepoResult<Customer>(null) { Type = RepoResultType.NotFound };
            var customerRepoMock = new Mock<ICustomersRespository>();
            customerRepoMock.Setup(repo => repo.UpdateCustomer(It.IsAny<Customer>())).Returns(repoResult);
            var customersController = new CustomersController(customerRepoMock.Object);

            //Act
            var result = customersController.PutCustomer(updatedCustomer.Id, updatedCustomer) as ActionResult;

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public void WhenDeleteCustomerCalled_WithValidCustomer_ReturnsOk(int validCustomerId)
        {
            //Arrange
            var repoResult = new RepoResult<Customer>(null) { Type = RepoResultType.Success };
            var customerRepoMock = new Mock<ICustomersRespository>();
            customerRepoMock.Setup(repo => repo.DeleteCustomer(It.IsAny<int>())).Returns(repoResult);
            var customersController = new CustomersController(customerRepoMock.Object);

            //Act
            var actualResult = customersController.DeleteCustomer(validCustomerId);

            //Assert
            Assert.IsType<OkResult>(actualResult);
        }

        [Theory]
        [InlineData(0)]
        public void WhenDeleteCustomerCalled_WithInvalidCustomer_ReturnsBadRequest(int invalidCustomerId)
        {
            //Arrange
            var customerRepoMock = new Mock<ICustomersRespository>();
            var customersController = new CustomersController(customerRepoMock.Object);

            //Act
            var result = customersController.DeleteCustomer(invalidCustomerId) as ActionResult;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData(6)]
        public void WhenDeleteCustomerCalled_WithNonExistantCustomer_ReturnsNotFound(int nonExistantId)
        {
            //Arrange
            var repoResult = new RepoResult<Customer>(null) { Type = RepoResultType.NotFound };
            var customerRepoMock = new Mock<ICustomersRespository>();
            customerRepoMock.Setup(repo => repo.DeleteCustomer(It.IsAny<int>())).Returns(repoResult);
            var customersController = new CustomersController(customerRepoMock.Object);

            //Act
            var result = customersController.DeleteCustomer(nonExistantId) as ActionResult;

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
