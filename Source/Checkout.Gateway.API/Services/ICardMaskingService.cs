namespace Checkout.Gateway.API.Services
{
    public interface ICardMaskingService
    {
        string MaskCardNumber(string cardNumber);
    }
}