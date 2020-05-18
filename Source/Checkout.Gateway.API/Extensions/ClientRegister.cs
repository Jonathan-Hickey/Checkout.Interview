using Checkout.Gateway.API.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Gateway.API.Extensions
{
    public static class ClientRegister
    {
        public static void RegisterClients(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<IBankOfIrelandClient, BankOfIrelandClient>();
        }
    }
}
