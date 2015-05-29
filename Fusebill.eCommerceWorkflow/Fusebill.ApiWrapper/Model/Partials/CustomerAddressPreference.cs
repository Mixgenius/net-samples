using System.Linq;

namespace Model
{
    public partial class CustomerAddressPreference
    {
        public Address BillingAddress
        {
            get { return Addresses.FirstOrDefault(a => a.AddressType == AddressType.Billing); }
        }

        public Address ShippingAddress
        {
            get {return Addresses.FirstOrDefault(a => a.AddressType == AddressType.Shipping); }
        }

        public Address ShippingAddressWithFallBack
        {
            get
            {
                if (UseBillingAddressAsShippingAddress)
                    return BillingAddress;

                return Addresses.FirstOrDefault(a => a.AddressType == AddressType.Shipping);
            }
        }
    }
}
