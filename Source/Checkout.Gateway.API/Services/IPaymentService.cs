using System;
using System.Threading.Tasks;
using Checkout.Gateway.API.Models.Requests;
using Checkout.Gateway.API.Models.Responses;

namespace Checkout.Gateway.API.Services
{
    public interface IPaymentService
    {
        Task<CardPaymentResponseDto> CreateCardPaymentAsync(Guid merchantId, CardPaymentRequestDto requestDto);
        Task<CardPaymentResponseDto> GetPayment(Guid merchantId, Guid paymentId);
    }
}