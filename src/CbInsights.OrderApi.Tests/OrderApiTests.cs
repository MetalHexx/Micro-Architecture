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
        [Theory]
        [ClassData(typeof(OrderExistingTestData))]
        public void WhenGetOrderCalled_AndOrderExists_ReturnsOkOrder(Order expectedOrder)
        {
            //Arrange     
            var repoResult = new RepoResult<Order>(expectedOrder) { Type = RepoResultType.Success };

            var orderRepoMock = new Mock<IOrdersRepository>();
            orderRepoMock.Setup(repo => repo.GetOrderById(It.IsAny<int>())).Returns(repoResult);
            var orderController = new OrdersController(orderRepoMock.Object);

            //Act
            var actualResult = orderController.GetOrder(1).Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(JsonConvert.SerializeObject(expectedOrder), JsonConvert.SerializeObject(actualResult.Value));
        }
                
        [Fact]
        public void WhenGetOrderCalled_WithNonExistantOrder_ReturnsNotFound()
        {
            //Arrange     
            var repoResult = new RepoResult<Order>(null) { Type = RepoResultType.NotFound };
            var orderRepoMock = new Mock<IOrdersRepository>();
            orderRepoMock.Setup(repo => repo.GetOrderById(It.IsAny<int>())).Returns(repoResult);
            var ordersController = new OrdersController(orderRepoMock.Object);

            //Act
            var actualResult = ordersController.GetOrder(0).Result;

            //Assert
            Assert.IsType<NotFoundResult>(actualResult);
        }

        [Theory]
        [ClassData(typeof(OrderListValidTestData))]
        public void WhenGetCustomerOrdersCalled_AndOrdersExist_ReturnOkOrders(List<Order> expectedOrders)
        {
            //Arrange
            var repoResult = new RepoResult<IEnumerable<Order>>(expectedOrders) { Type = RepoResultType.Success };
            var orderRepoMock = new Mock<IOrdersRepository>();
            orderRepoMock.Setup(repo => repo.GetOrdersByCustomerId(It.IsAny<int>())).Returns(repoResult);
            var orderController = new OrdersController(orderRepoMock.Object);

            //Act
            var actualResult = orderController.GetCustomerOrders(expectedOrders.First()
                .CustomerId).Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(JsonConvert.SerializeObject(expectedOrders), JsonConvert.SerializeObject(actualResult.Value));
        }

        [Fact]
        public void WhenGetCustomerOrdersCalled_AndOrdersDoNotExist_ReturnNotFound()
        {
            //Arrange
            var repoResult = new RepoResult<IEnumerable<Order>>(null){ Type = RepoResultType.NotFound };
            var repoMock = new Mock<IOrdersRepository>();
            repoMock.Setup(repo => repo.GetOrdersByCustomerId(It.IsAny<int>())).Returns(repoResult);
            var controller = new OrdersController(repoMock.Object);

            //Act
            var result = controller.GetCustomerOrders(6).Result as ActionResult;

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void WhenPostOrderCalled_WithValidOrder_ReturnsOkIdResult()
        {
            var newOrder = new Order
            {
                Id = 0,
                CustomerId = 1,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = 1,
                        Quantity = 1
                    }
                }
            };
            var createdOrder = new Order
            {
                Id = 1,
                CustomerId = 1,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = 1,
                        Quantity = 1
                    }
                }
            };
            //Arrange
            var expectedIdResult = new IdResult { Id = createdOrder.Id };
            var repoResult = new RepoResult<Order>(createdOrder) { Type = RepoResultType.Success };
            var repoMock = new Mock<IOrdersRepository>();
            repoMock.Setup(repo => repo.InsertOrder(It.IsAny<Order>())).Returns(repoResult);
            var controller = new OrdersController (repoMock.Object);

            //Act
            var actualResult = controller.PostOrder(newOrder).Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(JsonConvert.SerializeObject(expectedIdResult), 
                JsonConvert.SerializeObject(actualResult.Value));
        }

        [Theory]
        [ClassData(typeof(OrderInvalidTestData))]
        public void WhenPostOrderCalled_WithInvalidOrder_ReturnsBadRequest(Order invalidOrder)
        {
            //Arrange
            var repoMock = new Mock<IOrdersRepository>();
            var controller = new OrdersController(repoMock.Object);

            //Act
            var result = controller.PostOrder(invalidOrder).Result as ObjectResult;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [ClassData(typeof(OrderValidTestData))]

        public void WhenPutOrderCalled_WithValidOrder_ReturnsOkIdResult(Order expectedOrder)
        {
            //Arrange
            var expectedIdResult = new IdResult { Id = expectedOrder.Id };
            var repoResult = new RepoResult<Order>(expectedOrder) { Type = RepoResultType.Success };
            var repoMock = new Mock<IOrdersRepository>();
            repoMock.Setup(repo => repo.UpdateOrder(It.IsAny<Order>())).Returns(repoResult);
            var controller = new OrdersController(repoMock.Object);

            //Act
            var actualResult = controller.PutOrder(expectedOrder.Id, expectedOrder) as ActionResult;

            //Assert
            Assert.IsType<OkResult>(actualResult);
        }

        [Theory]
        [ClassData(typeof(OrderInvalidTestData))]
        public void WhenPutOrderCalled_WithInvalidOrder_ReturnsBadRequest(Order invalidOrder)
        {
            //Arrange
            var repoMock = new Mock<IOrdersRepository>();
            var controller = new OrdersController(repoMock.Object);

            //Act
            var result = controller.PutOrder(invalidOrder.Id, invalidOrder);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [ClassData(typeof(OrderExistingTestData))]
        public void WhenPutOrderCalled_WithNonExistentOrder_ReturnsNotFound(Order nonExistentOrder)
        {
            //Arrange
            var repoResult = new RepoResult<Order>(null) { Type = RepoResultType.NotFound };
            var mockRepo = new Mock<IOrdersRepository>();
            mockRepo.Setup(repo => repo.UpdateOrder(It.IsAny<Order>())).Returns(repoResult);
            var controller = new OrdersController(mockRepo.Object);

            //Act
            var result = controller.PutOrder(nonExistentOrder.Id, nonExistentOrder);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [ClassData(typeof(OrderExistingTestData))]
        public void WhenDeleteOrder_WithExistingOrder_OkResultReturned(Order existingOrder)
        {
            //Arrange
            var repoResult = new RepoResult<Order>(null) { Type = RepoResultType.Success };
            var mockRepo = new Mock<IOrdersRepository>();
            mockRepo.Setup(repo => repo.DeleteOrder(It.IsAny<int>())).Returns(repoResult);
            var controller = new OrdersController(mockRepo.Object);

            //Act
            var result = controller.DeleteOrder(existingOrder.Id);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Theory]
        [ClassData(typeof(OrderNonExistentTestData))]
        public void WhenDeleteOrderCalled_WithNonExistantOrder_NotFoundReturned(Order nonExistantOrder)
        {
            //Arrange
            var repoResult = new RepoResult<Order>(null) { Type = RepoResultType.NotFound };
            var mockRepo = new Mock<IOrdersRepository>();
            mockRepo.Setup(repo => repo.DeleteOrder(It.IsAny<int>())).Returns(repoResult);
            
            var controller = new OrdersController(mockRepo.Object);

            //Act
            var result = controller.DeleteOrder(nonExistantOrder.Id);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
