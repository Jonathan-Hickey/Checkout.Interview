using System;

namespace Checkout.Gateway.API.Client.Routes
{
    public interface IPaymentRoutes
    {
        Uri GetCreateCardPaymentUri();
        Uri GetPaymentDetailUri(Guid paymentId);
    }

    public class PaymentRoutes : IPaymentRoutes
    {
        private readonly Uri _cardPaymentUri;
        private string _baseUri;
        private readonly Guid _merchantId;

        public PaymentRoutes(Guid merchantId, string baseUri)
        {
            _baseUri = baseUri;
            //this is a hack so that we only need to create one instance of a uri and string
            //I expect that this part of the code has a lot of traffic and this may seems like a small improvement
            //I expect that this will have a huge performance boost for the clients code
            _merchantId = merchantId;
            _cardPaymentUri = new Uri($"{baseUri}/merchant/{_merchantId}/payment/card");
        }

        public Uri GetCreateCardPaymentUri()
        {
            return _cardPaymentUri;
        }

        public Uri GetPaymentDetailUri(Guid paymentId)
        {
            return new Uri($"{_baseUri}/merchant/{_merchantId}/payment/{paymentId}");
        }
    }
}
