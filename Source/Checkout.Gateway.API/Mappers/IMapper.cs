namespace Checkout.Gateway.API.Mappers
{
    public interface IMapper<TInput, TOutput>
    {
        TOutput Map(TInput input);
    }
}
