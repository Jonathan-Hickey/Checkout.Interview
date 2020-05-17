using System.Net;
using System.Net.Http;

namespace Checkout.Gateway.API.Tests.Fakes
{
    public class HttpFakeHandlerSettings
    {
        public string ResponseContent { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ExpectedUri { get; set; }
        public HttpMethod ExpectedHttpMethod { get; set; }
    }
}