using Checkout.Gateway.API.Mappers;
using Checkout.Gateway.API.Mappers.BankOfIreland;
using Checkout.Gateway.API.Models;
using Checkout.Gateway.API.Models.BankOfIreland;
using Checkout.Gateway.API.Models.Requests;
using Checkout.Gateway.API.Models.Responses;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Gateway.API.Extensions
{
    public static class MapperRegister
    {
        public static void RegisterMappers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMapper<CardPaymentRequestDto, BankOfIrelandPaymentRequest>, BankOfIrelandPaymentRequestMapper>();
            serviceCollection.AddSingleton<IMapper<Payment, CardPaymentResponseDto>, CardPaymentResponseMapper>();
            
            serviceCollection.AddSingleton<IPaymentStatusMapper, BankOfIrelandPaymentStatusMapper>();
        }
    }
}
