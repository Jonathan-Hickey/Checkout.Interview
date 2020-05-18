using System;
using System.Collections.Generic;
using System.Linq;
using Checkout.Gateway.API.Enum;
using Checkout.Gateway.API.Models;
using Checkout.Gateway.API.Models.Enums;

namespace Checkout.Gateway.API.FakeDataStores
{
    public interface IPaymentDataStore
    {
        Payment AddPayment(Guid merchantId, int addressId, int cardInformationId, AcquirerBank acquirerBank, decimal amount, string currencyCode);
        Payment GetPayment(Guid merchantId, Guid paymentId);

    }

    public class PaymentDataStore : IPaymentDataStore
    {
        public readonly List<Payment> _payments;
        
        public PaymentDataStore()
        {
            _payments = new List<Payment>();
        }


        public Payment GetPayment(Guid merchantId, Guid paymentId)
        {
            return _payments.Single(p => p.MerchantId == merchantId && p.PaymentId == paymentId);
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

            _payments.Add(card);

            return card;
        }
    }
}
