using Checkout.Gateway.API.Models;

namespace Checkout.Gateway.API.FakeDataStores
{
    public interface IAddressDataStore
    {
        Address GetAddress(int addressId);

        Address AddAddress();
    }
}