using CbInsights.OrdersApi.Controllers;
using CbInsights.OrdersApi.Models;
using CbInsights.OrdersApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CbInsights.OrderApi.Tests
{
    public class OrderApiTests
    {
        private IEnumerable<Order> _orderListTestData;
        private Order _orderTestData;
        private string _orderListTestDataJson;
        private string _orderTestDataJson;

        public OrderApiTests()
        {
            _orderListTestData = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    CustomerId = 0,
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = 3,
                            Quantity = 1
                        },
                        new OrderItem
                        {
                            ProductId = 4,
                            Quantity = 2
                        }
                    }
                },
                new Order
                {
                    Id = 2,
                    CustomerId = 1,
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = 0,
                            Quantity = 2
                        },
                        new OrderItem
                        {
                            ProductId = 1,
                            Quantity = 5
                        }
                    }
                }
            };
            _orderListTestDataJson = JsonConvert.SerializeObject(_orderListTestData);
            _orderTestData = _orderListTestData.First();
            _orderTestDataJson = JsonConvert.SerializeObject(_orderTestData);
        }

        [Fact]
        public void WhenGetOrderCalled_AndOrderExists_ReturnsOkOrder()
        {
            //Arrange     
            var expectedContent = _orderTestDataJson;
            var repoResult = new RepoResult<Order>(_orderTestData) { Type = RepoResultType.Success };

            var orderRepoMock = new Mock<IOrdersRepository>();
            orderRepoMock.Setup(repo => repo.GetOrderById(It.IsAny<int>())).Returns(repoResult);
            var orderController = new OrdersController(orderRepoMock.Object);

            //Act
            var actualResult = orderController.GetOrder(1).Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(expectedContent, JsonConvert.SerializeObject(actualResult.Value));
        }

        [Fact]
        public void WhenGetCustomerOrdersCalled_AndOrdersExist_ReturnOkOrders()
        {
            //Arrange
            var expectedResult = _orderListTestDataJson;
            var repoResult = new RepoResult<IEnumerable<Order>>(_orderListTestData) { Type = RepoResultType.Success };
            var orderRepoMock = new Mock<IOrdersRepository>();
            orderRepoMock.Setup(repo => repo.GetOrdersByCustomerId(It.IsAny<int>())).Returns(repoResult);
            var orderController = new OrdersController(orderRepoMock.Object);

            //Act
            var actualResult = orderController.GetCustomerOrders(_orderTestData.CustomerId).Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(expectedResult, JsonConvert.SerializeObject(actualResult.Value));
        }

                
        [Fact]
        public void WhenGetOrderCalled_WithNonExistantOrder_ReturnsNotFound()
        {
            //Arrange     
            var repoResult = new RepoResult<Order>(null) { Type = RepoResultType.NotFound };
            var orderRepoMock = new Mock<IOrdersRepository>();
            orderRepoMock.Setup(repo => repo.GetOrderById(0)).Returns(repoResult);
            var customersController = new CustomersController(orderRepoMock.Object);

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
            var result = customersController.PostCustomer(updatedCustomer).Result as ObjectResult;

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
            var result = customersController.PutCustomer(updatedCustomer.Id, updatedCustomer) as ObjectResult;

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
        [InlineData(0, 1)]
        public void WhenDeleteCustomerCalled_WithInvalidCustomer_ReturnsBadRequest(int invalidCustomerId, int expectedNumErrors)
        {
            //Arrange
            var customerRepoMock = new Mock<ICustomersRespository>();
            var customersController = new CustomersController(customerRepoMock.Object);

            //Act
            var result = customersController.DeleteCustomer(invalidCustomerId) as ActionResult;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(customersController.ModelState.ErrorCount, expectedNumErrors);

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
}
