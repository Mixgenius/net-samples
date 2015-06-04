using Fusebill.ApiWrapper.Dto.Interfaces;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Put
{
    public class OrderToCashCycle : EarningTiming, IBaseDto
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "pricingModel")]
        public PricingModel PricingModel { get; set; }
    }
}
