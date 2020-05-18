using System;
using System.Threading.Tasks;
using Checkout.Gateway.API.Models.Requests;
using Checkout.Gateway.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Checkout.Gateway.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("merchant/{merchantId}/payment")] 
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost]
        [Route("card")]
        public async Task<IActionResult> CreatePaymentAsync(Guid merchantId, CardPaymentRequestDto cardPaymentRequestDto)
        {
            if (User.GetMerchantId() != merchantId)
            {
                _logger.LogCritical($"Access token for merchantId {User.GetMerchantId()} might be compromised. Request made using different merchant Id {merchantId}");
                return Unauthorized();
            };

            if (cardPaymentRequestDto == null)
            {
                _logger.LogInformation("cardPaymentRequestDto is null");
                return BadRequest();
            }

            _logger.LogInformation("Incoming requestDto");

            var response = await _paymentService.CreateCardPaymentAsync(merchantId, cardPaymentRequestDto);

            return Created("", response); 
        }
    }
}
