using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using Common.ValidationAttributes;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Put
{
    public class PlanFrequency : BaseDto
    {
        [JsonProperty(PropertyName = "numberOfIntervals")]
      //  [NumberOfIntervalsValidation]
        public int NumberOfIntervals { get; set; }

        [JsonProperty(PropertyName = "interval")]
     //   [IntervalValidation]
        public string Interval { get; set; }

        [JsonProperty(PropertyName = "numberOfSubscriptions")]
        public int NumberOfSubscriptions { get; set; }

        [JsonProperty(PropertyName = "status")]
     //   [PlanFrequencyStatusValidation]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "setupFees")]
        public List<Price> SetupFees { get; set; }

        [JsonProperty(PropertyName = "charges")]
        public List<Price> Charges { get; set; }

        [JsonProperty(PropertyName = "isProrated")]
        public bool IsProrated { get; set; }

        [JsonProperty(PropertyName = "prorationGranularity")]
        public string ProrationGranularity { get; set; }
    }
}
