using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class SubscriptionCoupon : BaseDto
    {
        [JsonProperty(PropertyName = "subscriptionId")]
        public long SubscriptionId { get; set; }

        [JsonProperty(PropertyName = "couponCode")]
        public string CouponCode { get; set; }
    }
}
