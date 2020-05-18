
namespace Checkout.Gateway.API.Validation
{
    public interface ICardValidator
    {
        bool IsCardNumberValid(string cardNumber);
        bool IsExpiryDateValid(string expiryMonth, string expiryYear);
    }
}
