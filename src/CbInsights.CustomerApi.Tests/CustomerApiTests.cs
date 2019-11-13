using CbInsights.CustomerApi.Controllers;
using CbInsights.CustomersApi.Models;
using CbInsights.CustomersApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace CbInsights.CustomerApi.Tests
{
    public class CustomerApiTests
    {    
        [Fact]
        public void WhenGetCustomersCalled_ReturnsOkCustomerList()
        {
            //Arrange     
            var expectedContent = GetTestCustomerList();
            var repoResult = new RepoResult<IEnumerable<Customer>>(expectedContent) { Type = RepoResultType.Success };
            
            var customerRepoMock = new Mock<ICustomersRespository>();
            customerRepoMock.Setup(repo => repo.GetCustomers()).Returns(repoResult);
            var customersController = new CustomersController(customerRepoMock.Object);

            //Act
            var actualResult = customersController.GetCustomers().Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(JsonConvert.SerializeObject(expectedContent), JsonConvert.SerializeObject(actualResult.Value));
        }

        [Fact]
        public void WhenGetCustomerCalled_AndCustomerExists_ReturnsOkCustomer()
        {
            //Arrange     
            var expectedCustomer = GetTestCustomer();
            var repoResult = new RepoResult<Customer>(expectedCustomer) { Type = RepoResultType.Success };

            var customerRepoMock = new Mock<ICustomersRespository>();
            customerRepoMock.Setup(repo => repo.GetCustomer(It.IsAny<int>())).Returns(repoResult);
            var customersController = new CustomersController(customerRepoMock.Object);

            //Act
            var actualResult = customersController.GetCustomer(0).Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(JsonConvert.SerializeObject(expectedCustomer), JsonConvert.SerializeObject(actualResult.Value));
        }

        [Fact]
        public void WhenGetCustomerCalled_WithNonExistentCustomer_ReturnsNotFound()
        {
            //Arrange     
            var repoResult = new RepoResult<Customer>(null) { Type = RepoResultType.NotFound };
            var customerRepoMock = new Mock<ICustomersRespository>();
            customerRepoMock.Setup(repo => repo.GetCustomer(It.IsAny<int>())).Returns(repoResult);
            var customersController = new CustomersController(customerRepoMock.Object);

            //Act
            var actualResult = customersController.GetCustomer(0).Result;

            //Assert
            Assert.IsType<NotFoundResult>(actualResult);
        }

        [Theory]
        [InlineData("FirstName", "LastName", 0)]
        public void WhenPostCustomerCalled_WithValidCustomer_ReturnsOkIdResult(string firstName, string lastName, int id)
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
        [InlineData("FirstName", "LastName", 1, 1)]
        [InlineData("", "", 0, 2)]
        [InlineData("", "", 1, 3)]
        public void WhenPostCustomerCalled_WithInvalidCustomer_ReturnsBadRequest(string firstName, string lastName, int id, int expectedNumErrors)
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
            var result = customersController.PostCustomer(updatedCustomer).Result;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(customersController.ModelState.ErrorCount, expectedNumErrors);
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
        [InlineData("FirstName", "LastName", 0, 1)]
        [InlineData("", "", 1, 2)]
        [InlineData("", "", 0, 3)]
        public void WhenPutCustomerCalled_WithInvalidCustomer_ReturnsBadRequest(string firstName, string lastName, int id, int expectedNumErrors)
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
            var result = customersController.PutCustomer(updatedCustomer.Id, updatedCustomer);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(customersController.ModelState.ErrorCount, expectedNumErrors);
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
        [InlineData(6)]
        public void WhenDeleteCustomerCalled_WithNonExistantCustomer_ReturnsNotFound(int nonExistantId)
        {
            //Arrange
            var repoResult = new RepoResult<Customer>(null) { Type = RepoResultType.NotFound };
            var customerRepoMock = new Mock<ICustomersRespository>();
            customerRepoMock.Setup(repo => repo.DeleteCustomer(It.IsAny<int>())).Returns(repoResult);
            var customersController = new CustomersController(customerRepoMock.Object);

            //Act
            var result = customersController.DeleteCustomer(nonExistantId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private Customer GetTestCustomer()
        {
            return new Customer
            {
                Id = 0,
                FirstName = "William",
                LastName = "Pereira"
            };
        }

        private List<Customer> GetTestCustomerList()
        {
            return new List<Customer>()
            {
                new Customer
                {
                    Id = 1,
                    FirstName = "William",
                    LastName = "Pereira"
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Luke",
                    LastName = "Skywalker"
                }
            };
        }

    }
}
