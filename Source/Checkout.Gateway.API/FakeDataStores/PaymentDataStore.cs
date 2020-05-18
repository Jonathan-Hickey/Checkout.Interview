using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Gateway.API.Enum;
using Checkout.Gateway.API.Models;
using Checkout.Gateway.API.Models.Enums;

namespace Checkout.Gateway.API.FakeDataStores
{
    public class PaymentDataStore : IPaymentDataStore
    {
        public readonly List<Payment> Payments;
        
        public PaymentDataStore()
        {
            Payments = new List<Payment>();
        }

        public Task<Payment> GetPaymentAsync(Guid merchantId, Guid paymentId)
        {
            var payment = Payments.SingleOrDefault(p => p.MerchantId == merchantId && p.PaymentId == paymentId);
            return Task.FromResult(payment);
        }

        public Payment AddPayment(Guid merchantId, int addressId, int cardInformationId, AcquirerBank acquirerBank, decimal amount, string currencyCode)
        {
            var card = new Payment
            {
                PaymentId = Guid.NewGuid(),
                MerchantId = merchantId,
                BillingAddressId = addressId, 
                CardInformationId = cardInformationId,
                AcquirerBank = acquirerBank,
                PaymentStatus = PaymentStatus.Created,
                CurrencyCode = currencyCode,
                Amount = amount
            };

            Payments.Add(card);

            return card;
        }
    }
}
