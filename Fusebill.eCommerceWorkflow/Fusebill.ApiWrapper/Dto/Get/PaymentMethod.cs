using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public abstract class PaymentMethod : BaseDto
    {
        [JsonProperty(PropertyName = "customerId")]
        public long CustomerId { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "address1")]
        public string Address1 { get; set; }

        [JsonProperty(PropertyName = "address2")]
        public string Address2 { get; set; }

        [JsonProperty(PropertyName = "countryId")]
        public long? CountryId { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "stateId")]
        public long? StateId { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "postalZip")]
        public string PostalZip { get; set; }

        [JsonProperty(PropertyName = "isDefault")]
        public bool IsDefault { get; set; }

        [JsonProperty(PropertyName = "externalCustomerId")]
        public string ExternalCustomerId { get; set; }

        [JsonProperty(PropertyName = "externalCardId")]
        public string ExternalCardId { get; set; }

        [JsonProperty(PropertyName = "storedInFusebillVault")]
        public bool StoredInFusebillVault { get; set; }
    }
}
