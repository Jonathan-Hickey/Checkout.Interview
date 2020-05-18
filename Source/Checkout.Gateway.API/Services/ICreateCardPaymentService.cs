using System;
using System.Threading.Tasks;
using Checkout.Gateway.API.Models;
using Checkout.Gateway.API.Models.Requests;

namespace Checkout.Gateway.API.Services
{
    public interface ICreateCardPaymentService
    {
        Task<Payment> CreateCardPaymentAsync(Guid merchantId, CardPaymentRequestDto requestDto);
    }
}