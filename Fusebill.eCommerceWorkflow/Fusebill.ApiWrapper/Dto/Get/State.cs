using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class State 
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "subdivisionIsoCode")]
        public string SubdivisionISOCode { get; set; }

        [JsonProperty(PropertyName = "combinedIsoCode")]
        public string CombinedISOCode { get; set; }
    }
}
