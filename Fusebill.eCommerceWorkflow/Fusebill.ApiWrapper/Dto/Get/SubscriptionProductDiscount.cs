using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class SubscriptionProductDiscount : BaseDto
    {
        [JsonProperty(PropertyName = "discountType")]
        public string DiscountType { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "remainingUsagesUntilStart")]
        public int RemainingUsagesUntilStart { get; set; }

        [JsonProperty(PropertyName = "remainingUsage")]
        public int? RemainingUsage { get; set; }
    }
}
