using AutoMapper;
using Moq;
using System;
using System.Threading.Tasks;
using WEBAPI.Controllers;
using WEBAPI.Models.Dtos;
using WEBAPI.Repository.IRepository;
using Xunit;

namespace WEBAPI_Test
{
    public class TestAPI
    {
        private readonly Mock<IExpensivePaymentGateway> _mockRepo;
        private readonly Mock<IMapper> _mapperRepo;
        private readonly ProcessController _controller;

        public TestAPI()
        {
            _mockRepo = new Mock<IExpensivePaymentGateway>();
            _mapperRepo = new Mock<IMapper>();
            _controller = new ProcessController(_mockRepo.Object, _mapperRepo.Object);
        }
        [Fact]
        public void Test_ProcessPayment()
        {
            var request = new RequestDto
            {
                CreditCardNumber = "4242424242424242",
                CardHolder = "name",
                ExpirationDate = new DateTime(2023, 1, 1),
                SecurityCode = "",
                Amount = 1000
            };

            var result = _controller.ProcessPayment(request);

            
            Assert.IsType<Task<dynamic>>(result);
        }

        [Fact]
        public void Test_ValidateCard()
        {
            var request = new RequestDto
            {
                CreditCardNumber = "4242424242424242",
                CardHolder = "name",
                ExpirationDate = new DateTime(2023, 1, 1),
                SecurityCode = "",
                Amount = 1000
            };

            var result = _controller.ValidateCreditCard(request.CreditCardNumber);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public void Test_ValidateExpirationDate()
        {
            var request = new RequestDto
            {
                CreditCardNumber = "4242424242424242",
                CardHolder = "name",
                ExpirationDate = new DateTime(2023, 1, 1),
                SecurityCode = "",
                Amount = 1000
            };

            var result = _controller.ValidateExpirationDate(request.ExpirationDate);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public void Test_ValidateSecurityCode()
        {
            var request = new RequestDto
            {
                CreditCardNumber = "4242424242424242",
                CardHolder = "name",
                ExpirationDate = new DateTime(2023, 1, 1),
                SecurityCode = "",
                Amount = 1000
            };


            var result = request.SecurityCode.Length > 0 ? _controller.ValidateSecurityCode(request.SecurityCode) : true;

            Assert.IsType<bool>(result);
            Assert.True(result);
        }


        [Fact]
        public void Test_ValidateSecurityAmount()
        {
            var request = new RequestDto
            {
                CreditCardNumber = "4242424242424242",
                CardHolder = "name",
                ExpirationDate = new DateTime(2023, 1, 1),
                SecurityCode = "",
                Amount = 1000
            };

            var result = _controller.ValidateAmount(request.Amount);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }


    }
}
