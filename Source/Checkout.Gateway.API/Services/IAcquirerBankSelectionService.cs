using Checkout.Gateway.API.Enum;
using Checkout.Gateway.API.Models.Requests;

namespace Checkout.Gateway.API.Services
{
    public interface IAcquirerBankSelectionService
    {
        AcquirerBank GetAcquirerBankToUse(CardPaymentRequestDto cardPaymentRequestDto);
    }
}