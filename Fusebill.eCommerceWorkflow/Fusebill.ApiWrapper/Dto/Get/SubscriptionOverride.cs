using System;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class SubscriptionOverride : BaseDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "charge")]
        public Nullable<decimal> Charge { get; set; }

        [JsonProperty(PropertyName = "setupFee")]
        public Nullable<decimal> SetupFee { get; set; }
    }
}
