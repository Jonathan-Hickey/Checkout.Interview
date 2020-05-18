namespace Checkout.Gateway.API.Models.Models
{
    public class CardInformationDto
    {
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cvv { get; set; }
    }
}