using System.ComponentModel;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class SubscriptionProductEarningTiming
    {
        [JsonProperty(PropertyName = "earningTimingInterval")]
        [DisplayName("Earning Interval")]
        public string EarningTimingInterval { get; set; }

        [JsonProperty(PropertyName = "earningTimingType")]
        [DisplayName("Earning Timing")]
        public string EarningTimingType { get; set; }
    }
}