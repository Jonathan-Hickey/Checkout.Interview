namespace Checkout.Gateway.API.Models.Enums
{
    //https://docs.checkout.com/docs/response-codes#section-overview
    public enum PaymentStatus
    {
        Approved = 10000,
        SoftDecline = 20000,
        HardDecline = 30000,
        RiskResponses = 40000
    }
}
