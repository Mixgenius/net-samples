using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Post
{
    public abstract class PaymentMethod : BaseDto
    {
        [JsonProperty(PropertyName = "customerId")]
        public long CustomerId { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        [StringLength(50, ErrorMessage = "First name must not exceed 50 characters")]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        [StringLength(50, ErrorMessage = "Last name must not exceed 50 characters")]
        [Required]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "address1")]
        [StringLength(50, ErrorMessage = "Address 1 must not exceed 50 characters")]
        [DisplayName("Address 1")]
        public string Address1 { get; set; }

        [JsonProperty(PropertyName = "address2")]
        [StringLength(50, ErrorMessage = "Address 2 must not exceed 50 characters")]
        [DisplayName("Address 2")]
        public string Address2 { get; set; }

        [JsonProperty(PropertyName = "city")]
        [StringLength(50, ErrorMessage = "City must not exceed 50 characters")]
        [DisplayName("City")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "stateId")]
        [DisplayName("State")]
        public Nullable<long> StateId { get; set; }

        [JsonProperty(PropertyName = "countryId")]
        [DisplayName("Country")]
        public Nullable<long> CountryId { get; set; }

        [JsonProperty(PropertyName = "postalZip")]
        [StringLength(10, ErrorMessage = "Postal / Zip code must not exceed 10 characters")]
        [DisplayName("Postal / Zip code")]
        public string PostalZip { get; set; }

        [JsonProperty(PropertyName = "source")]
        [DisplayName("Source")]
        public string Source { get; set; }

       // [JsonProperty(PropertyName = "paymentCollectOptions")]
  //      public PaymentCollectOptions PaymentCollectOptions { get; set; }
    }
}
