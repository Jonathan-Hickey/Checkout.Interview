using Checkout.Gateway.API.Models.Enums;

namespace Checkout.Gateway.API.Mappers
{
    public interface IPaymentStatusMapper
    {
        PaymentStatus Map(string status);
    }
}
