using System;
using Checkout.Gateway.API.Models.Requests;
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

        public PaymentController(ILogger<PaymentController> logger )
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("card")]
        public IActionResult CreatePayment(Guid merchantId, CardPaymentRequest cardPaymentRequest)
        {
            if (User.GetMerchantId() != merchantId)
            {
                _logger.LogCritical($"Access token for merchantId {User.GetMerchantId()} might be compromised. Request made using different merchant Id {merchantId}");
                return Unauthorized();
            };

            if (cardPaymentRequest == null)
            {
                _logger.LogInformation("cardPaymentRequest is null");
                return BadRequest();
            }
            
            _logger.LogInformation("Incoming request");
            
            return Created("", ""); 
        }
    }
}
