using System;
using System.Threading.Tasks;
using Checkout.Gateway.API.Enum;
using Checkout.Gateway.API.FakeDataStores;
using Checkout.Gateway.API.Models;
using Checkout.Gateway.API.Models.Requests;

namespace Checkout.Gateway.API.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> AddPaymentAsync(AcquirerBank acquirerBank, Guid merchantId, CardPaymentRequestDto cardPaymentRequestDto);
        Task<Payment> UpdatePaymentAsync(Guid merchantId, Guid paymentId,string acquirerPaymentId, string acquirerPaymentStatus);
    }

    public class PaymentRepository : IPaymentRepository
    {
        private readonly IPaymentDataStore _paymentDataStore;
        private readonly IAddressDataStore _addressDataStore;
        private readonly ICardInformationDataStore _cardInformationDataStore;

        public PaymentRepository(IPaymentDataStore paymentDataStore, 
                                IAddressDataStore addressDataStore,
                                ICardInformationDataStore cardInformationDataStore)
        {
            _cardInformationDataStore = cardInformationDataStore;
            _addressDataStore = addressDataStore;
            _paymentDataStore = paymentDataStore;
        }

        public Task<Payment> AddPaymentAsync(AcquirerBank acquirerBank, Guid merchantId, CardPaymentRequestDto requestDto)
        {
            //this would be calling a stored proc which would do all this within a transaction at the database level 

            var address = _addressDataStore.AddAddress();
            var cardInformation = _cardInformationDataStore.AddCardInformation();
            var payment = _paymentDataStore.AddPayment(merchantId, address.AddressId, cardInformation.CardInformationId, acquirerBank, requestDto.Amount, requestDto.Currency);
            
            return Task.FromResult(payment);
        }

        public Task<Payment> UpdatePaymentAsync(Guid merchantId, Guid paymentId, string acquirerPaymentId, string acquirerPaymentStatus)
        {
            var payment = _paymentDataStore.GetPayment(merchantId, paymentId);
            payment.AcquirerPaymentId = acquirerPaymentId;
            payment.AcquirerPaymentStatus = acquirerPaymentStatus;

            return Task.FromResult(payment);
        }
    }
}
