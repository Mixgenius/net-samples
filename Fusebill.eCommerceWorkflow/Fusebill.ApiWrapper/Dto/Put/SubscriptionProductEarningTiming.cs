using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Put
{
    public class SubscriptionProductEarningTiming
    {
        [JsonProperty(PropertyName = "earningTimingInterval")]
        [Required]
        public string EarningTimingInterval { get; set; }

        [JsonProperty(PropertyName = "earningTimingType")]
        [Required]
        public string EarningTimingType { get; set; }
    }
}