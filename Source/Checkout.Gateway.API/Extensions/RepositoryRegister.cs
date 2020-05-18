using Checkout.Gateway.API.FakeDataStores;
using Checkout.Gateway.API.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Gateway.API.Extensions
{
    public static class RepositoryRegister
    {
        public static void RegisterRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAddressRepository, AddressRepository>();
            serviceCollection.AddSingleton<ICardRepository, CardRepository>();
            serviceCollection.AddSingleton<IPaymentRepository, PaymentRepository>();

            serviceCollection.AddSingleton<IAddressDataStore, AddressDataStore>();
            serviceCollection.AddSingleton<ICardInformationDataStore, CardInformationDataStore>();
            serviceCollection.AddSingleton<IPaymentDataStore, PaymentDataStore>();
        }
    }
}
