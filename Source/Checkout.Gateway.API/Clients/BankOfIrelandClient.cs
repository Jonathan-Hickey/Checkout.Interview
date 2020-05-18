using System;
using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Gateway.API.Models.BankOfIreland;
using Microsoft.Extensions.Logging;


namespace Checkout.Gateway.API.Clients
{
    public class BankOfIrelandClient : IBankOfIrelandClient
    {
        //We would inject a httpclient  and use it to make async calls the acquiring bank
        //I will just be new-ing up fake responses for the sake of the interview
        private readonly HttpClient _httpClient;
        private readonly ILogger<BankOfIrelandClient> _logger;

        public BankOfIrelandClient(HttpClient httpClient, ILogger<BankOfIrelandClient> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public Task<BankOfIrelandPaymentResponse> CreatePaymentAsync(BankOfIrelandPaymentRequest paymentRequest)
        {
            _logger.LogInformation("Making request");

            //var response = _httpClient.SendAsync(paymentRequest);

            var response = new BankOfIrelandPaymentResponse
            {
                PaymentId = Guid.NewGuid().ToString(),
                PaymentStatus = "Approved"
            };

            _logger.LogInformation("received response");
            return Task.FromResult(response);
        }
    }
}
