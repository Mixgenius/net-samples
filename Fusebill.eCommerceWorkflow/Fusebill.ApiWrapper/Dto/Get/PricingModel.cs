using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class PricingModel : BaseDto
    {
        [JsonProperty(PropertyName = "pricingModelType")]
        public string PricingModelType { get; set; }

        [JsonProperty(PropertyName = "quantityRanges")]
        public List<QuantityRange> QuantityRanges { get; set; }
    }
}
