using CustomersApi.Controllers;
using CustomersApi.Vaildators;
using CustomersApi.Models;
using CustomersApi.Repository;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace CustomersApi.Tests
{
    public class CustomerApiTests
    {
        private Mock<ICustomersRespository> _blankRepositoryMock;
        private Mock<IPutValidator> _successPutValidatorMock;
        private Mock<IPostValidator> _successPostValidatorMock;
        private Mock<IPutValidator> _failedPutValidatorMock;
        private Mock<IPostValidator> _failedPostValidatorMock;

        public CustomerApiTests()
        {
            var successValidatonResult =
                new ValidationResult(
                    new List<ValidationFailure>());

            var failedValidationResult =
                new ValidationResult(
                    new List<ValidationFailure> {
                        new ValidationFailure("test", "test") });

            _successPutValidatorMock = new Mock<IPutValidator>();
            _successPutValidatorMock.Setup(v => v.Validate(It.IsAny<Customer>())).Returns(successValidatonResult);
            _successPostValidatorMock = new Mock<IPostValidator>();
            _successPostValidatorMock.Setup(v => v.Validate(It.IsAny<Customer>())).Returns(successValidatonResult);
            _failedPutValidatorMock = new Mock<IPutValidator>();
            _failedPutValidatorMock.Setup(v => v.Validate(It.IsAny<Customer>())).Returns(failedValidationResult);
            _failedPostValidatorMock = new Mock<IPostValidator>();
            _failedPostValidatorMock.Setup(v => v.Validate(It.IsAny<Customer>())).Returns(failedValidationResult);

            _blankRepositoryMock = new Mock<ICustomersRespository>();
        }

        [Theory]
        [ClassData(typeof(CustomerListValidTestData))]
        public void WhenGetCustomersCalled_ReturnsOkCustomerList(List<Customer> expectedCustomers)
        {
            //Arrange     
            var repoResult = new RepoResult<IEnumerable<Customer>>(expectedCustomers) { Type = RepoResultType.Success };
            
            var repoMock = new Mock<ICustomersRespository>();
            repoMock.Setup(repo => repo.GetCustomers()).Returns(repoResult);
            var controller = new CustomersController(
                repoMock.Object, 
                _successPutValidatorMock.Object, 
                _successPostValidatorMock.Object);

            //Act
            var actualResult = controller.GetCustomers().Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(JsonConvert.SerializeObject(expectedCustomers), JsonConvert.SerializeObject(actualResult.Value));
        }

        [Theory]
        [ClassData(typeof(CustomerExistingTestData))]
        public void WhenGetCustomerCalled_AndCustomerExists_ReturnsOkCustomer(Customer expectedCustomer)
        {
            //Arrange     
            var repoResult = new RepoResult<Customer>(expectedCustomer) { Type = RepoResultType.Success };

            var repoMock = new Mock<ICustomersRespository>();
            repoMock.Setup(repo => repo.GetCustomer(It.IsAny<int>())).Returns(repoResult);
            var controller = new CustomersController(
                repoMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var actualResult = controller.GetCustomer(0).Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(JsonConvert.SerializeObject(expectedCustomer), JsonConvert.SerializeObject(actualResult.Value));
        }

        [Fact]
        public void WhenGetCustomerCalled_WithNonExistentCustomer_ReturnsNotFound()
        {
            //Arrange     
            var repoResult = new RepoResult<Customer>(null) { Type = RepoResultType.NotFound };
            var repoMock = new Mock<ICustomersRespository>();
            repoMock.Setup(repo => repo.GetCustomer(It.IsAny<int>())).Returns(repoResult);
            var controller = new CustomersController(
                repoMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var actualResult = controller.GetCustomer(0).Result;

            //Assert
            Assert.IsType<NotFoundResult>(actualResult);
        }

        [Theory]
        [ClassData(typeof(CustomerNonExistentTestData))]
        public void WhenPostCustomerCalled_WithValidCustomer_ReturnsOkIdResult(Customer nonExistingCustomer)
        {
            //Arrange
            var newCustomer = new Customer()
            {
                Id = nonExistingCustomer.Id,
                FirstName = nonExistingCustomer.FirstName,
                LastName = nonExistingCustomer.LastName
            };
            var repoCustomer = new Customer()
            {
                Id = 1,
                FirstName = nonExistingCustomer.FirstName,
                LastName = nonExistingCustomer.LastName
            };
            var expectedIdResult = JsonConvert.SerializeObject(new IdResult { Id = repoCustomer.Id });           
            var repoResult = new RepoResult<Customer>(repoCustomer) { Type = RepoResultType.Success };
            var repoMock = new Mock<ICustomersRespository>();
            repoMock.Setup(repo => repo.InsertCustomer(It.IsAny<Customer>())).Returns(repoResult);
            var controller = new CustomersController(
                repoMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var actualResult = controller.PostCustomer(newCustomer).Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(expectedIdResult, JsonConvert.SerializeObject(actualResult.Value));
        }

        [Theory]
        [ClassData(typeof(CustomerInvalidTestData))]
        public void WhenPostCustomerCalled_WithInvalidCustomer_ReturnsBadRequest(Customer invalidCustomer)
        {
            //Arrange
            var controller = new CustomersController(
                _blankRepositoryMock.Object,
                _failedPutValidatorMock.Object,
                _failedPostValidatorMock.Object);

            //Act
            var result = controller.PostCustomer(invalidCustomer).Result as ObjectResult;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);            
        }

        [Theory]
        [ClassData(typeof(CustomerExistingTestData))]
        public void WhenPutCustomerCalled_WithValidCustomer_ReturnsOk(Customer existingCustomer)
        {
            //Arrange
            var repoResult = new RepoResult<Customer>(existingCustomer) { Type = RepoResultType.Success };
            var repoMock = new Mock<ICustomersRespository>();
            repoMock.Setup(repo => repo.UpdateCustomer(It.IsAny<Customer>())).Returns(repoResult);
            var controller = new CustomersController(
                repoMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var actualResult = controller.PutCustomer(existingCustomer.Id, existingCustomer);

            //Assert
            Assert.IsType<OkResult>(actualResult);
        }

        [Fact]
        public void WhenPutCustomerCalled_WithInvalidCustomer_ReturnsBadRequest()
        {
            var controller = new CustomersController(
                _blankRepositoryMock.Object,
                _failedPutValidatorMock.Object,
                _failedPostValidatorMock.Object);

            //Act
            var result = controller.PutCustomer(0, new Customer());

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);            
        }


        [Theory]
        [ClassData(typeof(CustomerNonExistentTestData))]
        public void WhenPutCustomerCalled_WithNonExistantCustomer_ReturnsNotFound(Customer nonExistantCustomer)
        {
            //Arrange
            var repoResult = new RepoResult<Customer>(nonExistantCustomer) { Type = RepoResultType.NotFound };
            var repoMock = new Mock<ICustomersRespository>();
            repoMock.Setup(repo => repo.UpdateCustomer(It.IsAny<Customer>())).Returns(repoResult);
            var controller = new CustomersController(
                repoMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var result = controller.PutCustomer(nonExistantCustomer.Id, nonExistantCustomer) as ActionResult;

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [ClassData(typeof(CustomerExistingTestData))]
        public void WhenDeleteCustomerCalled_WithValidCustomer_ReturnsOk(Customer existingCustomer)
        {
            //Arrange
            var repoResult = new RepoResult<Customer>(existingCustomer) { Type = RepoResultType.Success };
            var repoMock = new Mock<ICustomersRespository>();
            repoMock.Setup(repo => repo.DeleteCustomer(It.IsAny<int>())).Returns(repoResult);
            var controller = new CustomersController(
                repoMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var actualResult = controller.DeleteCustomer(existingCustomer.Id);

            //Assert
            Assert.IsType<OkResult>(actualResult);
        }

        [Theory]
        [ClassData(typeof(CustomerExistingTestData))]
        public void WhenDeleteCustomerCalled_WithNonExistantCustomer_ReturnsNotFound(Customer nonExistentCustomer)
        {
            //Arrange
            var repoResult = new RepoResult<Customer>(nonExistentCustomer) { Type = RepoResultType.NotFound };
            var repoMock = new Mock<ICustomersRespository>();
            repoMock.Setup(repo => repo.DeleteCustomer(It.IsAny<int>())).Returns(repoResult);
            var controller = new CustomersController(
                repoMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var result = controller.DeleteCustomer(nonExistentCustomer.Id);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
