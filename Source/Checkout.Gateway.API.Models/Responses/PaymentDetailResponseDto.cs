using System;
using Checkout.Gateway.API.Models.Enums;

namespace Checkout.Gateway.API.Models.Responses
{
    public class PaymentDetailResponseDto
    {
        public Guid MerchantId { get; set; }
        public Guid PaymentId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string MaskedCardNumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}