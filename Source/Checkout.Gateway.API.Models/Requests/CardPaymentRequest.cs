using System;
using Checkout.Gateway.API.Models.Models;

namespace Checkout.Gateway.API.Models.Requests
{
    public class CardPaymentRequest
    {
        public CardInformation CardInformation { get; set; }
        
        public decimal Amount { get; set; }

        private string currency;

        public string Currency
        {
            get => currency;
            set
            {
                if (Enums.Currency.IsCurrencySupported(value))
                {
                    currency = value;
                }
                else
                {
                    throw new ArgumentException($"Unsupported currency code {value}");
                }
            }
        }
        
        public Address BillingAddress { get; set; }
    }
}
