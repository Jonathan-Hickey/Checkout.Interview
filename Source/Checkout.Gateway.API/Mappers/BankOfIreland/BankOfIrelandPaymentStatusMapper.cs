using System;
using Checkout.Gateway.API.Models.Enums;

namespace Checkout.Gateway.API.Mappers.BankOfIreland
{
    public class BankOfIrelandPaymentStatusMapper : IPaymentStatusMapper
    {
        public PaymentStatus Map(string status)
        {
            switch (status)
            {
                case "Approved":
                    return PaymentStatus.Approved;
                default:
                    throw new ArgumentException($"Unknown status {status}");
            }
        }
    }
}
