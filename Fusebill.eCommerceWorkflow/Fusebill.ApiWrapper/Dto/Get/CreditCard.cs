using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class CreditCard : PaymentMethod
    {
        [JsonProperty(PropertyName="maskedCardNumber")]
        public string MaskedCardNumber { get; set; }

        [JsonProperty(PropertyName = "cardType")]
        public string CardType { get; set; }

        [JsonProperty(PropertyName = "expirationMonth")]
        public int ExpirationMonth { get; set; }

        [JsonProperty(PropertyName = "expirationYear")]
        public int ExpirationYear { get; set; }
    }
}
