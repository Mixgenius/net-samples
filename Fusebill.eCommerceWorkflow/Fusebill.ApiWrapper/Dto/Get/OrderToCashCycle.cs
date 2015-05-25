using System.ComponentModel;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class OrderToCashCycle : BaseDto
    {
        [JsonProperty(PropertyName = "earningInterval")]
        public string EarningInterval { get; set; }

        [JsonProperty(PropertyName = "earningNumberOfIntervals")]
        public int? EarningNumberOfIntervals { get; set; }

        [JsonProperty(PropertyName = "earningTimingInterval")]
        [DisplayName("Earning Interval")]
        public string EarningTimingInterval { get; set; }

        [JsonProperty(PropertyName = "earningTimingType")]
        [DisplayName("Earning Timing")]
        public string EarningTimingType { get; set; }

        [JsonProperty(PropertyName = "pricingModel")]
        public PricingModel PricingModel { get; set; }
    }
}
