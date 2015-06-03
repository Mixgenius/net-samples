using System.Collections.Generic;
using Newtonsoft.Json;
//using Common.ValidationAttributes;
namespace Fusebill.ApiWrapper.Dto.Get
{
    public class PlanFrequency : BaseDto
    {
        [JsonProperty(PropertyName = "planRevisionId")]
        public long PlanRevisionId { get; set; }

        [JsonProperty(PropertyName = "numberOfIntervals")]
        public int NumberOfIntervals { get; set; }

        [JsonProperty(PropertyName = "interval")]
        public string Interval { get; set; }

        [JsonProperty(PropertyName = "numberOfSubscriptions")]
        public int NumberOfSubscriptions { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "setupFees")]
        public List<Price> SetupFees { get; set; }

        [JsonProperty(PropertyName = "charges")]
        public List<Price> Charges { get; set; }

        [JsonProperty(PropertyName = "isProrated")]
        public bool IsProrated { get; set; }

        [JsonProperty(PropertyName = "prorationGranularity")]
       // [ProrateGranularityValidation]
        public string ProrationGranularity { get; set; }

        [JsonProperty(PropertyName = "planFrequencyUniqueId")]
        public long PlanFrequencyUniqueId { get; set; }

        [JsonProperty(PropertyName = "customFields")]
        public List<PlanCustomField> CustomFields { get; set; }

        public bool ShouldSerializeCustomFields()
        {
            return CustomFields != null && CustomFields.Count > 0;
        }
    }
}
