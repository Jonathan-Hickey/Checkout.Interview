using System;

namespace Checkout.Gateway.API.Client.Routes
{
    public interface IPaymentRoutes
    {
        Uri GetCardPaymentUri();
    }

    public class PaymentRoutes : IPaymentRoutes
    {
        private readonly Uri _cardPaymentUri;

        public PaymentRoutes(Guid merchantId, string baseUri)
        {
            //this is a hack so that we only need to create one instance of a uri and string
            //I expect that this part of the code has a lot of traffic and this may seems like a small improvement
            //I expect that this will have a huge performance boost for the clients code
            _cardPaymentUri = new Uri($"{baseUri}/merchant/{merchantId}/payment/card");
        }

        public Uri GetCardPaymentUri()
        {
            return _cardPaymentUri;
        }
    }
}
