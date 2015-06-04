using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class Address : BaseDto
    {
        [JsonProperty(PropertyName = "customerAddressPreferenceId")]
        [DisplayName("Customer address preference Id")]
        public long CustomerAddressPreferenceId { get; set; }

        [Required(ErrorMessage="Please enter company name")]
        [JsonProperty(PropertyName = "companyName")]
        [DisplayName("Company name")]
        public string CompanyName { get; set; }

       [Required(ErrorMessage = "Please enter primary billing address")]
        [JsonProperty(PropertyName = "line1")]
        [DisplayName("Address line 1")]
        public string Line1 { get; set; }

         [Required(ErrorMessage = "Please enter secondary billing address")]
        [JsonProperty(PropertyName = "line2")]
        [DisplayName("Address line 2")]
        public string Line2 { get; set; }

        [Required(ErrorMessage="Please select country and state")]
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

        [Required(ErrorMessage="Please enter city name")]
        [JsonProperty(PropertyName = "city")]
        [DisplayName("City")]
        public string City { get; set; }

        [Required(ErrorMessage="Please enter postal code")]
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
