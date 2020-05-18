namespace Checkout.Gateway.API.Models
{
    public class CardInformation
    {
        public int CardInformationId { get; set; }
        public string CardNumberHash { get; set; }
        public string CardNumberMask { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}