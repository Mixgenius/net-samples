using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class CustomerSalesTrackingCode : BaseDto
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
