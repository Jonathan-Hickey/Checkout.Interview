using Checkout.Gateway.API.Enum;
using Checkout.Gateway.API.Models.Requests;

namespace Checkout.Gateway.API.Services
{
    public class AcquirerBankSelectionService : IAcquirerBankSelectionService
    {
        //I think this would be a pure function, which would allow easier testability
        public AcquirerBank GetAcquirerBankToUse(CardPaymentRequestDto cardPaymentRequestDto)
        {
            return AcquirerBank.BankOfIreland;
        }
    }
}