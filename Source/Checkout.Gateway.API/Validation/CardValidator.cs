using System;
using System.Globalization;
using Checkout.Gateway.API.Services;

namespace Checkout.Gateway.API.Validation
{
    public class CardValidator : ICardValidator 
    {
        private readonly IDatetimeService _datetimeService;

        public CardValidator(IDatetimeService datetimeService)
        {
            _datetimeService = datetimeService;
        }

        public bool IsCardNumberValid(string cardNumber)
        {
            //https://www.geeksforgeeks.org/luhn-algorithm/
            int nDigits = cardNumber.Length;

            int nSum = 0;
            bool isSecond = false;
            for (int i = nDigits - 1; i >= 0; i--)
            {

                int d = cardNumber[i] - '0';

                if (isSecond == true)
                    d = d * 2;

                // We add two digits to handle 
                // cases that make two digits  
                // after doubling 
                nSum += d / 10;
                nSum += d % 10;

                isSecond = !isSecond;
            }
            return (nSum % 10 == 0);
        }

        public bool IsExpiryDateValid(string expiryMonth, string expiryYear)
        {
            var date = $"01/{expiryMonth}/20{expiryYear}";
            var cardsExpiryDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                                          .AddMonths(1);
            return _datetimeService.GetUtc() < cardsExpiryDate;
        }
    }
}