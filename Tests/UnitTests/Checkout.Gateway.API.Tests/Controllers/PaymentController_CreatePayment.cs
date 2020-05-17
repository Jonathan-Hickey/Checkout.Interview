using System;
using Checkout.Gateway.API.Controllers;
using Checkout.Gateway.API.Models.Requests;
using Checkout.Gateway.API.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace Checkout.Gateway.API.Tests.Controllers
{
    [TestFixture]
    public class PaymentController_CreatePayment
    {
        [Test]
        public void When_MerchantId_Does_Not_Match_ValidAccess_Token_Then_Return_Unauthorized()
        {
            Guid merchantId = Guid.Parse("7e903c63-e75b-4788-b80d-d14d12fb5deb");
            
            var controller = CreatePaymentController(merchantId);
            
            var result = controller.CreatePayment(Guid.Parse("923c7a90-01d3-4151-974f-c87fd463de89"), null);

            result.Should().BeOfType<UnauthorizedResult>();
            var unauthorizedResult = result as UnauthorizedResult;

            unauthorizedResult.Should().NotBe(null);
            unauthorizedResult.StatusCode.Should().Be(401);
        }

        [Test] 
        public void When_ValidMerchant_And_No_CardPaymentDetails_Are_Passed_Then_Should_Return_BadRequest()
        {
            Guid merchantId = Guid.Parse("7e903c63-e75b-4788-b80d-d14d12fb5deb");

            var controller = CreatePaymentController(merchantId);

            var result = controller.CreatePayment(merchantId, null);

            result.Should().BeOfType<BadRequestResult>();
            var badRequestResult = result as BadRequestResult;

            badRequestResult.Should().NotBe(null);
            badRequestResult.StatusCode.Should().Be(400);
        }
        
        [Test]
        public void When_ValidMerchant_And_CardPaymentDetails_Are_Passed_Then_Should_Return_Created()
        {
            Guid merchantId = Guid.Parse("7e903c63-e75b-4788-b80d-d14d12fb5deb");

            var controller = CreatePaymentController(merchantId);

            var result = controller.CreatePayment(merchantId, new CardPaymentRequest());

            result.Should().BeOfType<CreatedResult>();
            var createdResult = result as CreatedResult;

            createdResult.Should().NotBe(null);
            createdResult.StatusCode.Should().Be(201);
        }

        private PaymentController CreatePaymentController(Guid merchantId)
        {
            var controller = new PaymentController(LoggerHelper.CreateLogger<PaymentController>());
            controller.ControllerContext = ControllerContextFactory.CreateControllerContextForClient(merchantId);
            return controller;
        }
    }
}