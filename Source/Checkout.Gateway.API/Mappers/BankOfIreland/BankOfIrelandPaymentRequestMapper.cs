using Checkout.Gateway.API.Models.BankOfIreland;
using Checkout.Gateway.API.Models.Requests;

namespace Checkout.Gateway.API.Mappers.BankOfIreland
{
    public class BankOfIrelandPaymentRequestMapper : IMapper<CardPaymentRequestDto, BankOfIrelandPaymentRequest>
    {
        public BankOfIrelandPaymentRequest Map(CardPaymentRequestDto input)
        {
            return new BankOfIrelandPaymentRequest();
        }
    }
}
