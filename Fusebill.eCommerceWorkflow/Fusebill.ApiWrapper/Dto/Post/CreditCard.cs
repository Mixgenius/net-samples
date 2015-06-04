using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fusebill.ApiWrapper.Dto.Interfaces;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Post
{
    public class CreditCard : PaymentMethod, ICreditCard
    {
        [JsonProperty(PropertyName = "cardNumber")]
        [StringLength(50, ErrorMessage = "Card number must not exceed 20 characters")]
        [Required]
        [DisplayName("Card number")]
        public string CardNumber { get; set; }

        [JsonProperty(PropertyName = "expirationMonth")]
        [DisplayName("Expiry month")]
        [Range(typeof(int), "1", "12", ErrorMessage = "Month must be between 1 and 12")]
        public int ExpirationMonth { get; set; }

        [JsonProperty(PropertyName = "expirationYear")]
        [DisplayName("Expiry year")]
        [Range(typeof(int), "0", "99", ErrorMessage = "Year must be between 0 and 99")]
        public int ExpirationYear { get; set; }

        [JsonProperty(PropertyName = "cvv")]
        [StringLength(5, ErrorMessage = "Security code must not exceed 5 characters")]
        [Required]
        [DisplayName("CVV")]
        public string Cvv { get; set; }
    }
}
