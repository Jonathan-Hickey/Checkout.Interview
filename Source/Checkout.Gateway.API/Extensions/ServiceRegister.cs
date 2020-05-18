using Checkout.Gateway.API.Services;
using Checkout.Gateway.API.Services.BankOfIreland;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Gateway.API.Extensions
{
    public static class ServiceRegister
    {
        public static void RegisterServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ICreateCardPaymentService, CreateCardPaymentService>();
            serviceCollection.AddSingleton<IDatetimeService, DatetimeService>();
            serviceCollection.AddSingleton<IPaymentService, PaymentService>();
            serviceCollection.AddSingleton<IAcquirerBankSelectionService, AcquirerBankSelectionService>();

            serviceCollection.AddSingleton<IAcquiringBankService, BankOfIrelandAcquiringBankService>();
            
        }
    }
}
