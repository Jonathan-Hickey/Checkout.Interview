using Checkout.Gateway.API.Models;
using Checkout.Gateway.API.Models.Responses;

namespace Checkout.Gateway.API.Mappers
{
    public class PaymentDetailResponseDtoMapper : IMapper<Payment, CardInformation, PaymentDetailResponseDto>
    {
        public PaymentDetailResponseDto Map(Payment payment, CardInformation cardInformation)
        {
            return new PaymentDetailResponseDto
            {
                PaymentStatus = payment.PaymentStatus,
                PaymentId = payment.PaymentId,
                MerchantId = payment.MerchantId,
                MaskedCardNumber = cardInformation.CardNumberMask,
                Month = cardInformation.ExpiryMonth,
                Year = cardInformation.ExpiryYear
            };
        }
    }
}
