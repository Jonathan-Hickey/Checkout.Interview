using System.Collections.Generic;
using System.Linq;
using Checkout.Gateway.API.Models;

namespace Checkout.Gateway.API.FakeDataStores
{
    public class AddressDataStore : IAddressDataStore
    {
        private readonly List<Address> _addresses;

        public AddressDataStore()
        {
            _addresses = new List<Address>();
        }

        public Address GetAddress(int addressId)
        {
            return _addresses.Single(a => a.AddressId == addressId);
        }

        public Address AddAddress()
        {
            //Not thread safe
            var maxId = 1;
            if (_addresses.Any())
            {
                maxId = _addresses.Max(a => a.AddressId);
            }
             

            var address = new Address
            {
                AddressId = maxId+1,

            };

            _addresses.Add(address);

            return address;
        }
    }
}
