using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Put
{
    public class SubscriptionProductOverride : BaseDto
    {
        [JsonProperty(PropertyName = "name")]
        [StringLength(100)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        [StringLength(70)]
        public string Description { get; set; }

    }
}
