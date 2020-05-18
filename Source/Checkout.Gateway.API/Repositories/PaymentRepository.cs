using System;
using System.Threading.Tasks;
using Checkout.Gateway.API.Enum;
using Checkout.Gateway.API.FakeDataStores;
using Checkout.Gateway.API.Models;
using Checkout.Gateway.API.Models.Requests;
using Checkout.Gateway.API.Services;

namespace Checkout.Gateway.API.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> AddPaymentAsync(AcquirerBank acquirerBank, Guid merchantId, CardPaymentRequestDto cardPaymentRequest);
        Task<Payment> UpdatePaymentAsync(Guid merchantId, Guid paymentId,string acquirerPaymentId, string acquirerPaymentStatus);

        Task<Payment> GetPaymentAsync(Guid merchantId, Guid paymentId);

    }

    public class PaymentRepository : IPaymentRepository
    {
        private readonly IPaymentDataStore _paymentDataStore;
        private readonly IAddressDataStore _addressDataStore;
        private readonly ICardInformationDataStore _cardInformationDataStore;
        private readonly ICardMaskingService _cardMaskingService;
        private readonly IHashService _hashService;


        public PaymentRepository(IPaymentDataStore paymentDataStore, 
                                IAddressDataStore addressDataStore,
                                ICardInformationDataStore cardInformationDataStore,
                                ICardMaskingService cardMaskingService,
                                IHashService hashService)
        {
            _hashService = hashService;
            _cardMaskingService = cardMaskingService;
            _cardInformationDataStore = cardInformationDataStore;
            _addressDataStore = addressDataStore;
            _paymentDataStore = paymentDataStore;
        }

        public Task<Payment> AddPaymentAsync(AcquirerBank acquirerBank, Guid merchantId, CardPaymentRequestDto cardPaymentRequest)
        {
            var maskedCardNumber = _cardMaskingService.MaskCardNumber(cardPaymentRequest.CardInformation.CardNumber);
            var card = cardPaymentRequest.CardInformation;
            var hashedCardNumber = _hashService.GetHash(card.CardNumber);
            
            //this would be calling a stored proc which would do all this within a transaction at the database level 
            var address = _addressDataStore.AddAddress();
            var cardInformation = _cardInformationDataStore.AddCardInformation(firstName: card.FirstName, lastName:card.LastName, expiryMonth: card.ExpiryMonth, expiryYear:card.ExpiryYear, hashedCardNumber:hashedCardNumber, maskedCardNumber:maskedCardNumber);
            var payment = _paymentDataStore.AddPayment(merchantId, address.AddressId, cardInformation.CardInformationId, acquirerBank, cardPaymentRequest.Amount, cardPaymentRequest.Currency);
            
            return Task.FromResult(payment);
        }

        public async Task<Payment> UpdatePaymentAsync(Guid merchantId, Guid paymentId, string acquirerPaymentId, string acquirerPaymentStatus)
        {
            var payment =  await _paymentDataStore.GetPaymentAsync(merchantId, paymentId);
            payment.AcquirerPaymentId = acquirerPaymentId;
            payment.AcquirerPaymentStatus = acquirerPaymentStatus;

            return payment;
        }

        public Task<Payment> GetPaymentAsync(Guid merchantId, Guid paymentId)
        {
            return _paymentDataStore.GetPaymentAsync(merchantId, paymentId);
        }
    }
}
