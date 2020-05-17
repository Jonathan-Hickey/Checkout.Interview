using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;

namespace Checkout.Gateway.API.Tests.Fakes
{
    public class FakeHttpHandler : DelegatingHandler
    {
        private readonly HttpFakeHandlerSettings _handlerSettings;

        public FakeHttpHandler(HttpFakeHandlerSettings handlerSettings)
        {
            _handlerSettings = handlerSettings;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.RequestUri.AbsoluteUri.Should().BeEquivalentTo(_handlerSettings.ExpectedUri);
            
            request.Method.Should().BeEquivalentTo(_handlerSettings.ExpectedHttpMethod);

            return Task.FromResult(new HttpResponseMessage()
            {
                Content = new StringContent(_handlerSettings.ResponseContent),
                StatusCode = _handlerSettings.StatusCode
            });
        }
    }
}