using System;
using System.Threading.Tasks;
using Checkout.Gateway.API.Enum;
using Checkout.Gateway.API.Models;

namespace Checkout.Gateway.API.FakeDataStores
{
    public interface IPaymentDataStore
    {
        Payment AddPayment(Guid merchantId, int addressId, int cardInformationId, AcquirerBank acquirerBank, decimal amount, string currencyCode);
        Task<Payment> GetPaymentAsync(Guid merchantId, Guid paymentId);

    }
}