using Newtonsoft.Json;
using System.Collections.Generic;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class Country
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "iso")]
        public string ISO { get; set; }

        [JsonProperty(PropertyName = "iso3")]
        public string ISO3 { get; set; }

        [JsonProperty(PropertyName = "states")]
        public List<State> States { get; set; }
    }
}
