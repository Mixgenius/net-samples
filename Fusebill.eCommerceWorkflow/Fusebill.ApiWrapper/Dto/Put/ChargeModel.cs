//using Common.ValidationAttributes;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Put
{
    public class ChargeModel : BaseDto
    {
        [JsonProperty(PropertyName = "chargeModelType")]
     //   [ChargeModelTypeValidation]
        public string ChargeModelType { get; set; }

        [JsonProperty(PropertyName = "chargeTimingType")]
     //   [ChargeTimingTypeValidation]
        public string ChargeTimingType { get; set; }

        [JsonProperty(PropertyName = "prorationGranularity")]
     //   [ProrateGranularityValidation]
        public string ProrationGranularity { get; set; }

        [JsonProperty(PropertyName = "prorateOnPositiveQuantity")]
        public bool ProrateOnPositiveQuantity { get; set; }

        [JsonProperty(PropertyName = "prorateOnNegativeQuantity")]
        public bool ProrateOnNegativeQuantity { get; set; }

        [JsonProperty(PropertyName = "reverseChargeOnNegativeQuantity")]
        public bool ReverseChargeOnNegativeQuantity { get; set; }
    }
}
