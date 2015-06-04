//using Common.ValidationAttributes;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fusebill.ApiWrapper.Dto.Post
{
    public class Address : BaseDto
    {
        [JsonProperty(PropertyName = "customerAddressPreferenceId")]
        [DisplayName("Customer address preference Id")]
        [Range(1, long.MaxValue, ErrorMessage = "Please specify a value for customerAddressPreferenceId")]
        public long CustomerAddressPreferenceId { get; set; }

        [JsonProperty(PropertyName = "companyName")]
        [DisplayName("Company name")]
        [StringLength(255)]
        public string CompanyName { get; set; }

        [JsonProperty(PropertyName = "line1")]
        [DisplayName("Address line 1")]
        [StringLength(60)]
        [Required]
        public string Line1 { get; set; }

        [JsonProperty(PropertyName = "line2")]
        [DisplayName("Address line 2")]
        [StringLength(60)]
        public string Line2 { get; set; }

        [JsonProperty(PropertyName = "countryId")]
        [DisplayName("Country")]
        public long CountryId { get; set; }

        [JsonProperty(PropertyName = "stateId")]
        [DisplayName("State")]
        public long? StateId { get; set; }

        [JsonProperty(PropertyName = "city")]
        [DisplayName("City")]
        [StringLength(50)]
        [Required]
        public string City { get; set; }

        [JsonProperty(PropertyName = "postalZip")]
        [DisplayName("Postal / Zip")]
        [StringLength(10)]
        [Required]
        public string PostalZip { get; set; }

        /// <summary>
        /// Billing and Shipping are allowed values. Shipping means that the billing address is also the shipping address
        /// </summary>
        [JsonProperty(PropertyName = "addressType")]
        [DisplayName("Address type")]
    //    [AddressTypeValidation]
        public string AddressType { get; set; }
    }
}
