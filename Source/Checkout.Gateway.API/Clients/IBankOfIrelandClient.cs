using System.Threading.Tasks;
using Checkout.Gateway.API.Models.BankOfIreland;

namespace Checkout.Gateway.API.Clients
{
    public interface IBankOfIrelandClient
    {
        Task<BankOfIrelandPaymentResponse> CreatePaymentAsync(BankOfIrelandPaymentRequest paymentRequest);
    }
}