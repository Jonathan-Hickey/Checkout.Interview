using System;
using System.Globalization;
using System.Threading.Tasks;
using Checkout.Gateway.API.Clients;
using Checkout.Gateway.API.Controllers;
using Checkout.Gateway.API.Enum;
using Checkout.Gateway.API.Mappers;
using Checkout.Gateway.API.Mappers.BankOfIreland;
using Checkout.Gateway.API.Models;
using Checkout.Gateway.API.Models.BankOfIreland;
using Checkout.Gateway.API.Models.Enums;
using Checkout.Gateway.API.Models.Models;
using Checkout.Gateway.API.Models.Requests;
using Checkout.Gateway.API.Models.Responses;
using Checkout.Gateway.API.Repositories;
using Checkout.Gateway.API.Services;
using Checkout.Gateway.API.Services.BankOfIreland;
using Checkout.Gateway.API.Tests.Helpers;
using Checkout.Gateway.API.Validation;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Checkout.Gateway.API.Tests.Controllers
{
    [TestFixture]
    public class PaymentController_CreatePayment
    {
        [Test]
        public async Task When_MerchantId_Does_Not_Match_ValidAccess_Token_Then_Return_Unauthorized()
        {
            Guid merchantId = Guid.Parse("7e903c63-e75b-4788-b80d-d14d12fb5deb");

            var controller = CreatePaymentController(merchantId);

            var result = await controller.CreatePaymentAsync(Guid.Parse("923c7a90-01d3-4151-974f-c87fd463de89"), null);

            result.Should().BeOfType<UnauthorizedResult>();
            var unauthorizedResult = result as UnauthorizedResult;

            unauthorizedResult.Should().NotBe(null);
            unauthorizedResult.StatusCode.Should().Be(401);
        }

        [Test] 
        public async Task When_ValidMerchant_And_No_CardPaymentDetails_Are_Passed_Then_Should_Return_BadRequest()
        {
            Guid merchantId = Guid.Parse("7e903c63-e75b-4788-b80d-d14d12fb5deb");
            
            var controller = CreatePaymentController(merchantId);

            var result = await controller.CreatePaymentAsync(merchantId, null);

            result.Should().BeOfType<BadRequestResult>();
            var badRequestResult = result as BadRequestResult;

            badRequestResult.Should().NotBe(null);
            badRequestResult.StatusCode.Should().Be(400);
        }
        
        [Test]
        public async Task When_ValidMerchant_And_CardPaymentDetails_Are_Passed_Then_Should_Return_Created()
        {
            Guid merchantId = Guid.Parse("7e903c63-e75b-4788-b80d-d14d12fb5deb");

            var cardPaymentRequest = new CardPaymentRequestDto
            {
                Amount = 100m,
                Currency = Currency.GBP,
                BillingAddressDto = new AddressDto()
                {
                    AddressLine1 = "Random Flat in",
                    AddressLine2 = "Canary Wharf",
                    City = "London",
                    Country = "England",
                    FirstName = "Joe",
                    LastName = "Blogs"
                },
                CardInformation = new CardInformationDto()
                {
                    //https://saijogeorge.com/dummy-credit-card-generator/
                    CardNumber = "366252948156588",
                    FirstName = "Joe",
                    LastName = "Blogs",
                    Cvv = "1011",
                    ExpiryMonth = "01",
                    ExpiryYear = "24"
                }
            };

            var paymentId = Guid.Parse("34aa9bf1-1df6-49c5-bd1f-e385a88ba2a9");

            var moqPaymentRepository = new Mock<IPaymentRepository>();
            moqPaymentRepository.Setup(p => p.AddPaymentAsync(AcquirerBank.BankOfIreland, merchantId, cardPaymentRequest))
                .ReturnsAsync(new Payment
                {
                    AcquirerBank = AcquirerBank.BankOfIreland, 
                    PaymentId = paymentId,
                    PaymentStatus = PaymentStatus.Created,
                    CardInformationId = 1, 
                    MerchantId = merchantId,
                    AcquirerPaymentStatus = null, 
                    AcquirerPaymentId = null, 
                    BillingAddressId = 1,
                    Amount = 100m,
                    CurrencyCode = Currency.GBP
                });
            
            var moqBankOfIrelandAcquiringClient = new Mock<IBankOfIrelandClient>();
            var bankOfIrelandPaymentId = "f5b9d23b-27a1-4724-9da5-be34e928e78f";
            var bankOfIrelandStatus = "Approved";
            moqBankOfIrelandAcquiringClient.Setup(c => c.CreatePaymentAsync(It.IsAny<BankOfIrelandPaymentRequest>()))
                .ReturnsAsync(new BankOfIrelandPaymentResponse
                {
                    PaymentId = bankOfIrelandPaymentId,
                    PaymentStatus = bankOfIrelandStatus
                });

            moqPaymentRepository.Setup(p => p.UpdatePaymentAsync(merchantId, paymentId, bankOfIrelandPaymentId, bankOfIrelandStatus))
                .ReturnsAsync(new Payment
                {
                    AcquirerBank = AcquirerBank.BankOfIreland,
                    PaymentId = paymentId,
                    PaymentStatus = PaymentStatus.Approved,
                    CardInformationId = 1,
                    MerchantId = merchantId,
                    AcquirerPaymentStatus = bankOfIrelandStatus,
                    AcquirerPaymentId = bankOfIrelandPaymentId,
                    BillingAddressId = 1,
                    Amount = 100m,
                    CurrencyCode = Currency.GBP
                });

            moqBankOfIrelandAcquiringClient.Setup(c => c.CreatePaymentAsync(It.IsAny<BankOfIrelandPaymentRequest>()))
                .ReturnsAsync(new BankOfIrelandPaymentResponse
                {
                    PaymentId = bankOfIrelandPaymentId,
                    PaymentStatus = bankOfIrelandStatus
                });

            var moqDatetimeService = new Mock<IDatetimeService>();

            moqDatetimeService.Setup(d => d.GetUtc()).Returns(DateTime.ParseExact("01/05/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture));

            var controller = CreatePaymentController(merchantId, moqPaymentRepository.Object, moqBankOfIrelandAcquiringClient.Object, moqDatetimeService.Object);

            var result = await controller.CreatePaymentAsync(merchantId, cardPaymentRequest);

            result.Should().BeOfType<CreatedResult>();
            var createdResult = result as CreatedResult;

            createdResult.Should().NotBe(null);
            createdResult.StatusCode.Should().Be(201);

            var expectedResponse = new CardPaymentResponseDto
            {
                MerchantId = merchantId,
                PaymentId = paymentId,
                PaymentStatus = PaymentStatus.Approved
            };
            
            var cardPaymentResponse = createdResult.Value as CardPaymentResponseDto;
            expectedResponse.Should().BeEquivalentTo(expectedResponse);
        }

        private PaymentController CreatePaymentController(Guid merchantId, IPaymentRepository paymentRepository, IBankOfIrelandClient bankOfIrelandClient, IDatetimeService datetimeService)
        {
            var bankOfIrelandPaymentRequestMapper = new BankOfIrelandPaymentRequestMapper();
            var bankOfIrelandAcquiringBankService = new BankOfIrelandAcquiringBankService(bankOfIrelandClient, bankOfIrelandPaymentRequestMapper, paymentRepository);
            var acquirerBankSelectionService = new AcquirerBankSelectionService();
            
            var createCardPaymentService = new CreateCardPaymentService(bankOfIrelandAcquiringBankService, paymentRepository, acquirerBankSelectionService);

            var cardPaymentResponseMapper = new CardPaymentResponseMapper();
            var paymentService = new PaymentService(createCardPaymentService, cardPaymentResponseMapper, null, null, null);

            var cardValidator = new CardValidator(datetimeService);
            var controller = new PaymentController(LoggerHelper.CreateLogger<PaymentController>(), paymentService, cardValidator);
            controller.ControllerContext = ControllerContextFactory.CreateControllerContextForClient(merchantId);
            
            return controller;
        }

        private PaymentController CreatePaymentController(Guid merchantId)
        {
            return CreatePaymentController(merchantId, null, null, null);
        }
    }
}