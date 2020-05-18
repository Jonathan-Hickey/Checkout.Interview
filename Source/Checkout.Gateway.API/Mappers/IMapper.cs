namespace Checkout.Gateway.API.Mappers
{
    public interface IMapper<TInput, TOutput>
    {
        TOutput Map(TInput input);
    }

    public interface IMapper<TInput1, TInput2, TOutput>
    {
        TOutput Map(TInput1 input1, TInput2 input2);
    }
}
