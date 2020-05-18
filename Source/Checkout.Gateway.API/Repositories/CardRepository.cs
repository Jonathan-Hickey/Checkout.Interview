using System.Threading.Tasks;
using Checkout.Gateway.API.FakeDataStores;
using Checkout.Gateway.API.Models;

namespace Checkout.Gateway.API.Repositories
{
    public interface ICardRepository
    {
        Task<CardInformation> GetCardInformationAsync(int cardInformationId);
    }

    public class CardRepository : ICardRepository
    {
        private readonly ICardInformationDataStore _cardInformationDataStore;

        public CardRepository(ICardInformationDataStore cardInformationDataStore)
        {
            _cardInformationDataStore = cardInformationDataStore;
        }

        public Task<CardInformation> GetCardInformationAsync(int cardInformationId)
        {
            return Task.FromResult(_cardInformationDataStore.GetCardInformation(cardInformationId));
        }
    }
}
