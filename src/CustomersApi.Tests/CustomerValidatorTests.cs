using CustomersApi.Application.Validations;
using CustomersApi.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CustomersApi.Tests
{
    public class CustomerValidatorTests
    {
        [Theory]
        [ClassData(typeof(CustomerValidTestData))]
        public void WhenCustomerValidatorCalled_WithValidCustomer_ReturnsIsValid(Customer customer)
        {
            //Arrange
            var validator = new CustomerValidator();
            //Act
            var results = validator.Validate(customer);
            //Assert
            Assert.True(results.IsValid);
        }

        [Theory]
        [ClassData(typeof(CustomerInvalidTestData))]
        public void WhenCustomerValidatorCalled_WithInvalidCustomer_ReturnsNotValid(Customer customer)
        {
            //Arrange
            var validator = new CustomerValidator();
            //Act
            var results = validator.Validate(customer);
            //Assert
            Assert.False(results.IsValid);
        }

        [Theory]
        [ClassData(typeof(CustomerExistingTestData))]
        public void WhenPutValidatorCalled_WithExistingCustomer_ReturnsValid(Customer customer)
        {
            //Arrange
            var validator = new PutValidator();
            //Act
            var results = validator.Validate(customer);
            //Assert
            Assert.True(results.IsValid);
        }


        [Theory]
        [ClassData(typeof(CustomerNonExistentTestData))]
        public void WhenPutValidatorCalled_WithNonExistentCustomer_ReturnsNotValid(Customer customer)
        {
            //Arrange
            var validator = new PutValidator();
            //Act
            var results = validator.Validate(customer);
            //Assert
            Assert.False(results.IsValid);
        }

        [Theory]
        [ClassData(typeof(CustomerExistingTestData))]
        public void WhenPostValidatorCalled_WithExistingCustomer_ReturnsInvalid(Customer customer)
        {
            //Arrange
            var validator = new PostValidator();
            //Act
            var results = validator.Validate(customer);
            //Assert
            Assert.False(results.IsValid);
        }


        [Theory]
        [ClassData(typeof(CustomerNonExistentTestData))]
        public void WhenPostValidatorCalled_WithNonExistentCustomer_ReturnsValid(Customer customer)
        {
            //Arrange
            var validator = new PostValidator();
            //Act
            var results = validator.Validate(customer);
            //Assert
            Assert.True(results.IsValid);
        }
    }
}
