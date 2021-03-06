﻿using System;
using System.Threading.Tasks;
using Checkout.Gateway.API.Models.Requests;
using Checkout.Gateway.API.Services;
using Checkout.Gateway.API.Validation;
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
        private readonly ICardValidator _cardValidator;

        public PaymentController(ILogger<PaymentController> logger, 
                                 IPaymentService paymentService,
                                 ICardValidator cardValidator)
        {
            _cardValidator = cardValidator;
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

            if (cardPaymentRequestDto == null 
                || !_cardValidator.IsCardNumberValid(cardPaymentRequestDto.CardInformation.CardNumber)
                || !_cardValidator.IsExpiryDateValid(cardPaymentRequestDto.CardInformation.ExpiryMonth, cardPaymentRequestDto.CardInformation.ExpiryYear))
            {
                //Todo give back a real error message
                _logger.LogInformation("cardPaymentRequestDto is null");
                return BadRequest();
            }

            _logger.LogInformation("Incoming requestDto");

            var cardPayment = await _paymentService.CreateCardPaymentAsync(merchantId, cardPaymentRequestDto);
            
            var request = HttpContext.Request;

            return Created($"{request.Scheme}://{request.Host.Value}/merchant/{merchantId}/payment/{cardPayment.PaymentId}", cardPayment); 
        }


        [HttpGet]
        [Route("{paymentId}")]
        public async Task<IActionResult> GetPaymentDetailsAsync(Guid merchantId, Guid paymentId)
        {
            _logger.LogInformation($"Merchant {merchantId} is getting payment details for {paymentId}");

            var payment = await _paymentService.GetPaymentAsync(merchantId, paymentId);

            if (payment == null)
            {
                _logger.LogInformation($"Unable to find paymentId: {paymentId} for MerchantId: {merchantId}");
                return NotFound($"Unable to find payment {paymentId}");
            }

            return Ok(payment);
        }
    }
}
