using System;
using System.Text.RegularExpressions;

namespace Checkout.Gateway.API.Services
{
    public class CardMaskingService : ICardMaskingService
    {
        public string MaskCardNumber(string cardNumber)
        {
            https://stackoverflow.com/a/31000285
            var firstDigits = cardNumber.Substring(0, 6);
            var lastDigits = cardNumber.Substring(cardNumber.Length - 4, 4);

            var requiredMask = new String('X', cardNumber.Length - firstDigits.Length - lastDigits.Length);

            var maskedString = string.Concat(firstDigits, requiredMask, lastDigits);
            return Regex.Replace(maskedString, ".{4}", "$0 ", RegexOptions.Compiled);
        }

    }
}