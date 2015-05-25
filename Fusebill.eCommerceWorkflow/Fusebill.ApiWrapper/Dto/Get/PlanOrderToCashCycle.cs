using System.Collections.Generic;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class PlanOrderToCashCycle : OrderToCashCycle
    {
        [JsonProperty(PropertyName = "planFrequencyId")]
        public long PlanFrequencyId { get; set; }

        [JsonProperty(PropertyName = "planProductId")]
        public long PlanProductId { get; set; }

        [JsonProperty(PropertyName = "numberOfIntervals")]
        public int NumberOfIntervals { get; set; }

        [JsonProperty(PropertyName = "interval")]
        public string Interval { get; set; }        

        [JsonProperty(PropertyName = "chargeModels")]
        public List<ChargeModel> ChargeModels { get; set; }

        [JsonProperty(PropertyName = "customFields")]
        public List<PlanProductCustomField> CustomFields { get; set; }

        public bool ShouldSerializeCustomFields()
        {
            return CustomFields != null && CustomFields.Count > 0;
        }
    }
}
