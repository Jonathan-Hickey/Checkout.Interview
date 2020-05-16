using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Checkout.Gateway.MerchantExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5002");

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "MerchantApi",
                ClientSecret = "CodingInterview",
                Scope = "PaymentGateway"
            });


            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                Console.WriteLine("ERROR");
                Console.WriteLine("ERROR");

                Console.ReadLine();
                return;
            }

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("https://localhost:5001/payment");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }

            Console.ReadLine();
        }
    }
}
