using System;
using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Gateway.API.Client;
using Checkout.Gateway.API.Client.Routes;
using Checkout.Gateway.API.IntegrationTests.Helpers;
using Checkout.Gateway.API.Models.Enums;
using Checkout.Gateway.API.Models.Models;
using Checkout.Gateway.API.Models.Requests;
using Checkout.Gateway.API.Models.Responses;
using FluentAssertions;
using NUnit.Framework;

namespace Checkout.Gateway.API.IntegrationTests.PaymentController
{
    [TestFixture]
    public class PaymentController_GetPaymentDetailsAsync
    {

        [Test]
        public async Task When_A_Payment_Is_Created_And_GetPaymentDetails_Is_Called_Then_PaymentDetails_Should_Be_Returned()
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

            var payment = await paymentClient.CreateCardPaymentAsync(cardPaymentRequest);

            var paymentDetails = await paymentClient.GetPaymentDetailsAsync(payment.PaymentId);


            var expectedResponse = new PaymentDetailResponseDto
            {
                PaymentStatus = payment.PaymentStatus,
                PaymentId = payment.PaymentId,
                MerchantId = payment.MerchantId,
                Year = "24",
                Month = "01",
                MaskedCardNumber = "3662 52XX XXX6 588"
            };

            paymentDetails.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
