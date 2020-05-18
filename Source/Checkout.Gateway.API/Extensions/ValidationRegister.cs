using Checkout.Gateway.API.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Gateway.API.Extensions
{
    public static class ValidationRegister
    {
        public static void RegisterValidators(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ICardValidator, CardValidator>();
            
        }
    }
}
