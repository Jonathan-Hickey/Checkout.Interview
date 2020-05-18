using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Checkout.Gateway.API.IntegrationTests.Helpers
{
    public static class TokenHelper
    {
        /*
         * Due to identity server using all in-memory stores, and not persisting the data or tokens
         * We need to call RequestClientCredentialsTokenAsync each time we run the test
         * Ideally this token is  given to the merchant and the merchant would never call the Identity server.
         * This would allows us to control the life time of the token
         */

        public static async Task<string> GetReferenceAccessToken()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5002");

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "b074e29b-54bc-4085-a97d-5a370cafa598",
                ClientSecret = "CodingInterview",
                Scope = "PaymentGateway"
            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            return tokenResponse.AccessToken;
        }
    }
}
