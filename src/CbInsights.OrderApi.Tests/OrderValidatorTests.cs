using CbInsights.OrdersApi.Models;
using CbInsights.OrdersApi.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CbInsights.OrderApi.Tests
{
    public class OrderValidatorTests
    {
        [Theory]
        [ClassData(typeof(OrderValidTestData))]
        public void WhenOrderValidatorCalled_WithValidOrder_ReturnsIsValid(Order order)
        {
            //Arrange
            var validator = new OrderValidator();
            //Act
            var results = validator.Validate(order);
            //Assert
            Assert.True(results.IsValid);
        }

        [Theory]
        [ClassData(typeof(OrderInvalidTestData))]
        public void WhenOrderValidatorCalled_WithInvalidOrder_ReturnsNotValid(Order order)
        {
            //Arrange
            var validator = new OrderValidator();
            //Act
            var results = validator.Validate(order);
            //Assert
            Assert.True(!results.IsValid);
        }

        [Theory]
        [ClassData(typeof(OrderExistingTestData))]
        public void WhenPutValidatorCalled_WithExistingOrder_ReturnsValid(Order order)
        {
            //Arrange
            var validator = new PutValidator();
            //Act
            var results = validator.Validate(order);
            //Assert
            Assert.True(results.IsValid);
        }


        [Theory]
        [ClassData(typeof(OrderNonExistentTestData))]
        public void WhenPutValidatorCalled_WithNonExistentOrder_ReturnsNotValid(Order order)
        {
            //Arrange
            var validator = new PutValidator();
            //Act
            var results = validator.Validate(order);
            //Assert
            Assert.True(!results.IsValid);
        }

        [Theory]
        [ClassData(typeof(OrderExistingTestData))]
        public void WhenPostValidatorCalled_WithExistingOrder_ReturnsInvalid(Order order)
        {
            //Arrange
            var validator = new PostValidator();
            //Act
            var results = validator.Validate(order);
            //Assert
            Assert.True(!results.IsValid);
        }


        [Theory]
        [ClassData(typeof(OrderNonExistentTestData))]
        public void WhenPostValidatorCalled_WithNonExistentOrder_ReturnsValid(Order order)
        {
            //Arrange
            var validator = new PostValidator();
            //Act
            var results = validator.Validate(order);
            //Assert
            Assert.True(results.IsValid);
        }
    }
}
