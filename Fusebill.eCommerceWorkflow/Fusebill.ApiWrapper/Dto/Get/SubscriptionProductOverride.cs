using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class SubscriptionProductOverride : BaseDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}
