using CustomersApi.Controllers;
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
using CustomersApi.Domain;
using CustomersApi.Infrastructure.Persistance;
using CustomersApi.Application.Validations;
using MediatR;
using CustomersApi.Application.Queries;
using System.Threading.Tasks;
using System.Threading;
using CustomersApi.Application.Commands;

namespace CustomersApi.Tests
{
    public class CustomerApiTests
    {
        private Mock<ICustomersRepository> _blankRepositoryMock;
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

            _blankRepositoryMock = new Mock<ICustomersRepository>();
        }

        [Theory]
        [ClassData(typeof(CustomerListValidTestData))]
        public async Task WhenGetCustomersCalled_ReturnsOkCustomerList(List<Customer> expectedCustomers)
        {
            //Arrange     
            var repoResult = new RepoResult<IEnumerable<Customer>>(expectedCustomers) { Type = RepoResultType.Success };            
            
            var mediatorMock = new Mock<IMediator>();

            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllCustomers>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(repoResult);


            var controller = new CustomersController(                
                _blankRepositoryMock.Object,
                mediatorMock.Object,
                _successPutValidatorMock.Object, 
                _successPostValidatorMock.Object);

            //Act
            var actualResult = (await controller.GetCustomers(default)).Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(JsonConvert.SerializeObject(expectedCustomers), JsonConvert.SerializeObject(actualResult.Value));
        }

        [Theory]
        [ClassData(typeof(CustomerExistingTestData))]
        public async Task WhenGetCustomerCalled_AndCustomerExists_ReturnsOkCustomer(Customer expectedCustomer)
        {
            //Arrange     
            var repoResult = new RepoResult<Customer>(expectedCustomer) { Type = RepoResultType.Success };

            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetCustomerById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(repoResult);

            var repoMock = new Mock<ICustomersRepository>();
            repoMock.Setup(repo => repo.GetCustomer(It.IsAny<int>())).Returns(repoResult);
            var controller = new CustomersController(                
                _blankRepositoryMock.Object,
                mediatorMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var actualResult = (await controller.GetCustomer(0, default)).Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(JsonConvert.SerializeObject(expectedCustomer), JsonConvert.SerializeObject(actualResult.Value));
        }

        [Fact]
        public async Task WhenGetCustomerCalled_WithNonExistentCustomer_ReturnsNotFound()
        {
            //Arrange     
            var repoResult = new RepoResult<Customer>(null) { Type = RepoResultType.NotFound };
            
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetCustomerById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(repoResult);

            var repoMock = new Mock<ICustomersRepository>();
            repoMock.Setup(repo => repo.GetCustomer(It.IsAny<int>())).Returns(repoResult);
            var controller = new CustomersController(
                repoMock.Object,
                mediatorMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var actualResult = await controller.GetCustomer(0, default);

            //Assert
            Assert.IsType<NotFoundResult>(actualResult.Result);
        }

        [Theory]
        [ClassData(typeof(CustomerNonExistentTestData))]
        public async Task WhenPostCustomerCalled_WithValidCustomer_ReturnsOkId(Customer nonExistingCustomer)
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
            var expectedId = repoCustomer.Id;            
            var repoResult = new RepoResult<Customer>(repoCustomer) { Type = RepoResultType.Success };

            var mediatorMock = new Mock<IMediator>();
            mediatorMock
               .Setup(m => m.Send(It.IsAny<CreateCustomer>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(repoResult);

            var controller = new CustomersController(
                _blankRepositoryMock.Object,
                mediatorMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var actualResult = (await controller.PostCustomer(newCustomer, default)).Result as ObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actualResult);
            Assert.Equal(expectedId, actualResult.Value);
        }

        [Theory]
        [ClassData(typeof(CustomerInvalidTestData))]
        public async Task WhenPostCustomerCalled_WithInvalidCustomer_ReturnsBadRequest(Customer invalidCustomer)
        {
            //Arrange
            var mediatorMock = new Mock<IMediator>();

            var controller = new CustomersController(
                _blankRepositoryMock.Object,
                mediatorMock.Object,
                _failedPutValidatorMock.Object,
                _failedPostValidatorMock.Object);

            //Act
            var result = await controller.PostCustomer(invalidCustomer, default);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result as ObjectResult);            
        }

        [Theory]
        [ClassData(typeof(CustomerExistingTestData))]
        public void WhenPutCustomerCalled_WithValidCustomer_ReturnsOk(Customer existingCustomer)
        {
            //Arrange
            var repoResult = new RepoResult<Customer>(existingCustomer) { Type = RepoResultType.Success };
            var mediatorMock = new Mock<IMediator>();
            var repoMock = new Mock<ICustomersRepository>();
            repoMock.Setup(repo => repo.UpdateCustomer(It.IsAny<Customer>())).Returns(repoResult);
            var controller = new CustomersController(
                repoMock.Object,
                mediatorMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var actualResult = controller.PutCustomer(existingCustomer.Id, existingCustomer, default);

            //Assert
            Assert.IsType<OkResult>(actualResult);
        }

        [Fact]
        public void WhenPutCustomerCalled_WithInvalidCustomer_ReturnsBadRequest()
        {
            var mediatorMock = new Mock<IMediator>();

            var controller = new CustomersController(
                _blankRepositoryMock.Object,
                mediatorMock.Object,
                _failedPutValidatorMock.Object,
                _failedPostValidatorMock.Object);

            //Act
            var result = controller.PutCustomer(0, new Customer(), default);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);            
        }


        [Theory]
        [ClassData(typeof(CustomerNonExistentTestData))]
        public void WhenPutCustomerCalled_WithNonExistantCustomer_ReturnsNotFound(Customer nonExistantCustomer)
        {
            //Arrange
            var mediatorMock = new Mock<IMediator>();
            var repoResult = new RepoResult<Customer>(nonExistantCustomer) { Type = RepoResultType.NotFound };
            var repoMock = new Mock<ICustomersRepository>();
            repoMock.Setup(repo => repo.UpdateCustomer(It.IsAny<Customer>())).Returns(repoResult);
            var controller = new CustomersController(
                repoMock.Object,
                mediatorMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var result = controller.PutCustomer(nonExistantCustomer.Id, nonExistantCustomer, default) as ActionResult;

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [ClassData(typeof(CustomerExistingTestData))]
        public void WhenDeleteCustomerCalled_WithValidCustomer_ReturnsOk(Customer existingCustomer)
        {
            //Arrange
            var repoResult = new RepoResult<Customer>(existingCustomer) { Type = RepoResultType.Success };
            var repoMock = new Mock<ICustomersRepository>();
            var mediatorMock = new Mock<IMediator>();
            repoMock.Setup(repo => repo.DeleteCustomer(It.IsAny<int>())).Returns(repoResult);
            var controller = new CustomersController(
                repoMock.Object,
                mediatorMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var actualResult = controller.DeleteCustomer(existingCustomer.Id, default);

            //Assert
            Assert.IsType<OkResult>(actualResult);
        }

        [Theory]
        [ClassData(typeof(CustomerExistingTestData))]
        public void WhenDeleteCustomerCalled_WithNonExistantCustomer_ReturnsNotFound(Customer nonExistentCustomer)
        {
            //Arrange
            var repoResult = new RepoResult<Customer>(nonExistentCustomer) { Type = RepoResultType.NotFound };
            var repoMock = new Mock<ICustomersRepository>();
            repoMock.Setup(repo => repo.DeleteCustomer(It.IsAny<int>())).Returns(repoResult);
            var mediatorMock = new Mock<IMediator>();
            var controller = new CustomersController(
                repoMock.Object,
                mediatorMock.Object,
                _successPutValidatorMock.Object,
                _successPostValidatorMock.Object);

            //Act
            var result = controller.DeleteCustomer(nonExistentCustomer.Id, default);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
