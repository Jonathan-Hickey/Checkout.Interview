using System;
using Checkout.Gateway.API.Models.Enums;

namespace Checkout.Gateway.API.Models.Responses
{
    public class CardPaymentResponseDto
    {
        public Guid MerchantId { get; set; }
        public Guid PaymentId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
