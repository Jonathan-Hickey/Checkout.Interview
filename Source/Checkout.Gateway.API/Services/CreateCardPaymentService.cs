using System;
using System.Threading.Tasks;
using Checkout.Gateway.API.Enum;
using Checkout.Gateway.API.Models;
using Checkout.Gateway.API.Models.Requests;
using Checkout.Gateway.API.Repositories;

namespace Checkout.Gateway.API.Services
{
    public class CreateCardPaymentService : ICreateCardPaymentService
    {
        private readonly IAcquiringBankService _bankOfIrelandAcquiringBankService;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IAcquirerBankSelectionService _acquirerBankSelectionService;

        public CreateCardPaymentService(IAcquiringBankService bankOfIrelandAcquiringBankService, 
            IPaymentRepository paymentRepository, 
            IAcquirerBankSelectionService acquirerBankSelectionService)
        {
            _paymentRepository = paymentRepository;
            _bankOfIrelandAcquiringBankService = bankOfIrelandAcquiringBankService;
            _acquirerBankSelectionService = acquirerBankSelectionService;
        }

        public async Task<Payment> CreateCardPaymentAsync(Guid merchantId, CardPaymentRequestDto requestDto)
        {
            //Depending on the card payment we may want to route to different acquirers 
            //in this example we only have bankOfIreland but in future we can add more here
            var acquirerBankToUse = _acquirerBankSelectionService.GetAcquirerBankToUse(requestDto);

            var payment = await _paymentRepository.AddPaymentAsync(acquirerBankToUse, merchantId, requestDto);
            
            switch (acquirerBankToUse)
            {
                case AcquirerBank.BankOfIreland:
                    return await _bankOfIrelandAcquiringBankService.CreatePaymentAsync(merchantId: merchantId, paymentId: payment.PaymentId, requestDto);
                default:
                    throw new ArgumentException($"Unable able to support AcquirerBank {acquirerBankToUse}");
            }
        }
    }
}