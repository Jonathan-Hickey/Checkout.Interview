using System;
using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Gateway.API.Client;
using Checkout.Gateway.API.Client.Exceptions;
using Checkout.Gateway.API.Client.Routes;
using Checkout.Gateway.API.IntegrationTests.Helpers;
using Checkout.Gateway.API.Models.Enums;
using Checkout.Gateway.API.Models.Models;
using Checkout.Gateway.API.Models.Requests;
using FluentAssertions;
using NUnit.Framework;

namespace Checkout.Gateway.API.IntegrationTests.PaymentController
{
    [TestFixture]
    public class PaymentController_CreatePayment
    {
        [Test]
        public async Task When_ValidCerditCard_Information_Used_Then_Payment_Is_Created()
        {
            var accessToken = await TokenHelper.GetReferenceAccessToken();

            Guid merchantId = Guid.Parse("b074e29b-54bc-4085-a97d-5a370cafa598");
            var baseUri = "https://localhost:5001";
            var apiClient = new HttpClient();
            var paymentClient = new PaymentClient(apiClient, new PaymentRoutes(merchantId, baseUri), accessToken);

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
                    CardNumber = "366252948156588",
                    FirstName = "Joe",
                    LastName = "Blogs",
                    Cvv = "1011",
                    ExpiryMonth = "01",
                    ExpiryYear = "24"
                }
            };

            try
            {
                var response = await paymentClient.CreateCardPaymentAsync(cardPaymentRequest);
                response.PaymentId.Should().NotBeEmpty();
                response.MerchantId.Should().Be(merchantId);
                response.PaymentStatus.Should().Be(PaymentStatus.Created);
            }
            catch (CheckoutGatewayException gatewayException)
            {
                Assert.Fail();
            }
        }
    }

    
}