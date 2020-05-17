using System;
using System.Net.Http;

namespace Checkout.Gateway.API.Client.Exceptions
{
    public class CheckoutGatewayException :  Exception
    {

        //This class still needs to be implemented 
        //The idea is that if a request fails then we should throw an exception
        //this exception will contain all the error information and will allow the consumer 
        //to program against the exception and handle fail requests.
        //Our code will handle the happy path of the request, the consumer will handle all failed request
        private readonly HttpResponseMessage _httpResponseMessage;

        public CheckoutGatewayException(HttpResponseMessage httpResponseMessage)
        {
            _httpResponseMessage = httpResponseMessage;
        }
    }
}
