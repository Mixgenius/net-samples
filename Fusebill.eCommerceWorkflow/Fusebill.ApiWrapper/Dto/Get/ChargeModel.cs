using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class ChargeModel : BaseDto
    {
        [JsonProperty(PropertyName = "chargeModelType")]
        public string ChargeModelType { get; set; }

        [JsonProperty(PropertyName = "chargeTimingType")]
        public string ChargeTimingType { get; set; }

        [JsonProperty(PropertyName = "prorationGranularity")]
        public string ProrationGranularity { get; set; }

        [JsonProperty(PropertyName = "prorateOnPositiveQuantity")]
        public bool ProrateOnPositiveQuantity { get; set; }

        [JsonProperty(PropertyName = "prorateOnNegativeQuantity")]
        public bool ProrateOnNegativeQuantity { get; set; }

        [JsonProperty(PropertyName = "reverseChargeOnNegativeQuantity")]
        public bool ReverseChargeOnNegativeQuantity { get; set; }
    }
}
