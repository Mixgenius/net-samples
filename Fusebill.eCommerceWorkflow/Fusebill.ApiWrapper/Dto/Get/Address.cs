using Newtonsoft.Json;
using System.ComponentModel;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class Address : BaseDto
    {
        [JsonProperty(PropertyName = "customerAddressPreferenceId")]
        [DisplayName("Customer address preference Id")]
        public long CustomerAddressPreferenceId { get; set; }

        [JsonProperty(PropertyName = "companyName")]
        [DisplayName("Company name")]
        public string CompanyName { get; set; }

        [JsonProperty(PropertyName = "line1")]
        [DisplayName("Address line 1")]
        public string Line1 { get; set; }

        [JsonProperty(PropertyName = "line2")]
        [DisplayName("Address line 2")]
        public string Line2 { get; set; }

        [JsonProperty(PropertyName = "countryId")]
        [DisplayName("Country")]
        public long CountryId { get; set; }

        [JsonProperty(PropertyName = "country")]
        [DisplayName("Country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "stateId")]
        [DisplayName("State")]
        public long? StateId { get; set; }

        [JsonProperty(PropertyName = "state")]
        [DisplayName("State")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "city")]
        [DisplayName("City")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "postalZip")]
        [DisplayName("Postal / Zip")]
        public string PostalZip { get; set; }

        [JsonProperty(PropertyName = "addressType")]
        [DisplayName("Address type")]
        public string AddressType { get; set; }

        public string CityAndState()
        {
            if (string.IsNullOrEmpty(City))
            {
                return State;
            }
            if (string.IsNullOrEmpty(State))
            {
                return City;
            }
            return City + ", " + State;
        }
    }
}
