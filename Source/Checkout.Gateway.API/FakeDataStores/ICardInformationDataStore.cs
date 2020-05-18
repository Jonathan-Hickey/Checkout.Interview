using Checkout.Gateway.API.Models;

namespace Checkout.Gateway.API.FakeDataStores
{
    public interface ICardInformationDataStore
    {
        CardInformation AddCardInformation(string firstName,
            string lastName,
            string expiryMonth,
            string expiryYear,
            string hashedCardNumber,
            string maskedCardNumber);

        CardInformation GetCardInformation(int cardInformationId);

    }
}