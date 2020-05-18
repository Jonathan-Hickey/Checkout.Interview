namespace Checkout.Gateway.API.Services
{
    public interface IHashService
    {
        string GetHash(string input);
    }
}