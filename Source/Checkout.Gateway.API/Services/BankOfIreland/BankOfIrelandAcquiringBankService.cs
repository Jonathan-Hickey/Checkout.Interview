using System;
using System.Threading.Tasks;
using Checkout.Gateway.API.Clients;
using Checkout.Gateway.API.Mappers;
using Checkout.Gateway.API.Models;
using Checkout.Gateway.API.Models.BankOfIreland;
using Checkout.Gateway.API.Models.Requests;
using Checkout.Gateway.API.Repositories;

namespace Checkout.Gateway.API.Services.BankOfIreland
{
    public class BankOfIrelandAcquiringBankService : IAcquiringBankService
    {
        private readonly IBankOfIrelandClient _bankOfIrelandClient;
        private readonly IMapper<CardPaymentRequestDto, BankOfIrelandPaymentRequest> _cardPaymentRequestMapper;
        private readonly IPaymentRepository _paymentRepository;

        public BankOfIrelandAcquiringBankService(IBankOfIrelandClient bankOfIrelandClient, 
            IMapper<CardPaymentRequestDto, BankOfIrelandPaymentRequest> cardPaymentRequestMapper,
            IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
            _cardPaymentRequestMapper = cardPaymentRequestMapper;
            _bankOfIrelandClient = bankOfIrelandClient;
        }

        public async Task<Payment> CreatePaymentAsync(Guid merchantId, Guid paymentId, CardPaymentRequestDto cardPaymentRequestDto)
        {
            var request = _cardPaymentRequestMapper.Map(cardPaymentRequestDto);

            var bankOfIrelandResponse = await _bankOfIrelandClient.CreatePaymentAsync(request);

            return await _paymentRepository.UpdatePaymentAsync(paymentId: paymentId,
                                                        merchantId: merchantId,
                                                        acquirerPaymentId: bankOfIrelandResponse.PaymentId,
                                                        acquirerPaymentStatus: bankOfIrelandResponse.PaymentStatus);
        }
    }
}
