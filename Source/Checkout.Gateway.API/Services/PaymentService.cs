using System;
using System.Threading.Tasks;
using Checkout.Gateway.API.Mappers;
using Checkout.Gateway.API.Models;
using Checkout.Gateway.API.Models.Requests;
using Checkout.Gateway.API.Models.Responses;
using Checkout.Gateway.API.Repositories;

namespace Checkout.Gateway.API.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ICreateCardPaymentService _cardPaymentService;
        private readonly IMapper<Payment, CardPaymentResponseDto> _cardPaymentResponseMapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IMapper<Payment, CardInformation, PaymentDetailResponseDto> _paymentDetailResponseMapper;

        public PaymentService(ICreateCardPaymentService cardPaymentService, 
            IMapper<Payment, CardPaymentResponseDto> cardPaymentResponseMapper,
            IPaymentRepository paymentRepository,
            ICardRepository cardRepository,
            IMapper<Payment, CardInformation, PaymentDetailResponseDto> paymentDetailResponseMapper)
        {
            _paymentDetailResponseMapper = paymentDetailResponseMapper;
            _cardRepository = cardRepository;
            _paymentRepository = paymentRepository;
            _cardPaymentResponseMapper = cardPaymentResponseMapper;
            _cardPaymentService = cardPaymentService;
        }

        public async Task<CardPaymentResponseDto> CreateCardPaymentAsync(Guid merchantId, CardPaymentRequestDto requestDto)
        {
            var payment = await _cardPaymentService.CreateCardPaymentAsync(merchantId, requestDto);
            return _cardPaymentResponseMapper.Map(payment);
        }

        public async Task<PaymentDetailResponseDto> GetPaymentAsync(Guid merchantId, Guid paymentId)
        {
            var payment = await _paymentRepository.GetPaymentAsync(merchantId, paymentId);
            var cardInformation =  await _cardRepository.GetCardInformationAsync(payment.CardInformationId);
            return _paymentDetailResponseMapper.Map(payment, cardInformation);
        }
    }
}
