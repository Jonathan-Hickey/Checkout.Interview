using System;
using Checkout.Gateway.API.Enum;
using Checkout.Gateway.API.Models.Enums;

namespace Checkout.Gateway.API.Models
{
    public class Payment
    {
        public Guid MerchantId { get; set; }
        public Guid PaymentId { get; set; }
        public AcquirerBank AcquirerBank { get; set; }
        public string AcquirerPaymentId { get; set; }
        public string AcquirerPaymentStatus { get; set; }
        public int BillingAddressId { get; set; }
        public int CardInformationId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
