using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Put
{
    public class PlanOrderToCashCycle : OrderToCashCycle, IValidatableObject
    {
        [JsonProperty(PropertyName = "planFrequencyId")]
        public long PlanFrequencyId { get; set; }
        
        [JsonProperty(PropertyName = "planProductId")]
        public long PlanProductId { get; set; }

        [JsonProperty(PropertyName = "chargeModels")]
        public List<ChargeModel> ChargeModels { get; set; }
    }
}
