using System.Collections.Generic;
using System.Linq;
using Checkout.Gateway.API.Models;

namespace Checkout.Gateway.API.FakeDataStores
{
    public class CardInformationDataStore : ICardInformationDataStore
    {
        public readonly List<CardInformation> _cards;

        public CardInformationDataStore()
        {
            _cards = new List<CardInformation>();
        }


        public CardInformation GetCardInformation(int cardInformationId)
        {
            return _cards.Single(c => c.CardInformationId == cardInformationId);
        }

        public CardInformation AddCardInformation(string firstName,
                                                  string lastName,
                                                  string expiryMonth, 
                                                  string expiryYear,
                                                  string hashedCardNumber, 
                                                  string maskedCardNumber)
        {
            //Not thread safe
            var maxId = 1;
            if (_cards.Any())
            {
                maxId = _cards.Max(c => c.CardInformationId);
            }

            var card = new CardInformation
            {
                CardInformationId = maxId,
                LastName = lastName,
                FirstName = firstName,
                ExpiryMonth = expiryMonth,
                ExpiryYear = expiryYear,
                CardNumberHash = hashedCardNumber,
                CardNumberMask = maskedCardNumber,
            };

            _cards.Add(card);

            return card;
        }
    }
}
