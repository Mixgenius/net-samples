using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Post
{
    public class CustomerActivation : BaseDto
    {
        [JsonProperty(PropertyName = "customerId")]
        public long CustomerId { get; set; }

        [JsonProperty(PropertyName = "activateAllSubscriptions")]
        public bool ActivateAllSubscriptions { get; set; }

        [JsonProperty(PropertyName = "activateAllDraftPurchases")]
        public bool ActivateAllDraftPurchases { get; set; }

        [JsonProperty(PropertyName = "temporarilyDisableAutoPost")]
        public bool TemporarilyDisableAutoPost { get; set; }
    }
}
