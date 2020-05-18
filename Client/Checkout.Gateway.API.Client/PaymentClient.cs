using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Checkout.Gateway.API.Models.Requests;
using Checkout.Gateway.API.Models.Responses;
using System.Text.Json;
using Checkout.Gateway.API.Client.Exceptions;
using Checkout.Gateway.API.Client.Routes;

namespace Checkout.Gateway.API.Client
{

    public interface IPaymentClient
    {
        Task<CardPaymentResponseDto> CreateCardPaymentAsync(CardPaymentRequestDto cardPaymentRequestDto);
    }

    public class PaymentClient : IPaymentClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _accessToken;
        private readonly IPaymentRoutes _paymentRoutes;

        private const string Bearer = "Bearer";

        public PaymentClient(HttpClient httpClient, IPaymentRoutes paymentRoutes, string accessToken)
        {
            _paymentRoutes = paymentRoutes;

            _accessToken = accessToken;
            _httpClient = httpClient;
        }


        public async Task<CardPaymentResponseDto> CreateCardPaymentAsync(CardPaymentRequestDto cardPaymentRequestDto)
        {
           
            var response = await _httpClient.SendAsync(new HttpRequestMessage
            {
                Headers =
                {
                    Authorization = new AuthenticationHeaderValue(Bearer, _accessToken),
                },
                RequestUri = _paymentRoutes.GetCardPaymentUri(),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonSerializer.Serialize(cardPaymentRequestDto), Encoding.UTF8, "application/json")
            });

            if (!response.IsSuccessStatusCode)
            {
                throw new CheckoutGatewayException(response);
            }

            return JsonSerializer.Deserialize<CardPaymentResponseDto>(await response.Content.ReadAsStringAsync());
        }
    }
}
