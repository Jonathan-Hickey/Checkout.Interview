using Checkout.Gateway.API.Models;
using Checkout.Gateway.API.Models.Responses;

namespace Checkout.Gateway.API.Mappers
{
    public class CardPaymentResponseMapper : IMapper<Payment, CardPaymentResponseDto>
    {
        public CardPaymentResponseDto Map(Payment payment)
        {
            return new CardPaymentResponseDto
            {
                PaymentId = payment.PaymentId,
                MerchantId = payment.MerchantId,
                PaymentStatus = payment.PaymentStatus
            };
        }
    }
}
