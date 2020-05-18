using System.Threading.Tasks;
using Checkout.Gateway.API.FakeDataStores;
using Checkout.Gateway.API.Models;

namespace Checkout.Gateway.API.Repositories
{
    public interface IAddressRepository
    {
        Task<Address> GetAddressAsync(int addressId);
    }

    public class AddressRepository : IAddressRepository
    {
        private readonly IAddressDataStore _addressDataStore;

        public AddressRepository(IAddressDataStore addressDataStore)
        {
            _addressDataStore = addressDataStore;
        }

        public Task<Address> GetAddressAsync(int addressId)
        {
            return Task.FromResult(_addressDataStore.GetAddress(addressId));
        }
    }
}
