using System;
using System.Threading.Tasks;
using Checkout.Gateway.API.Mappers;
using Checkout.Gateway.API.Models;
using Checkout.Gateway.API.Models.Requests;
using Checkout.Gateway.API.Models.Responses;

namespace Checkout.Gateway.API.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ICreateCardPaymentService _cardPaymentService;
        private readonly IMapper<Payment, CardPaymentResponseDto> _cardPaymentResponseMapper;

        public PaymentService(ICreateCardPaymentService cardPaymentService, IMapper<Payment, CardPaymentResponseDto> cardPaymentResponseMapper)
        {
            _cardPaymentResponseMapper = cardPaymentResponseMapper;
            _cardPaymentService = cardPaymentService;
        }

        public async Task<CardPaymentResponseDto> CreateCardPaymentAsync(Guid merchantId, CardPaymentRequestDto requestDto)
        {
            var payment = await _cardPaymentService.CreateCardPaymentAsync(merchantId, requestDto);
            return _cardPaymentResponseMapper.Map(payment);
        }

        public Task<CardPaymentResponseDto> GetPayment(Guid merchantId, Guid paymentId)
        {
            throw new NotImplementedException();
        }
    }
}
