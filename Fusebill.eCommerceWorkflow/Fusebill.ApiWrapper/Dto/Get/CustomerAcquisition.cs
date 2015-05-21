using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class CustomerAcquisition : BaseDto
    {
        [JsonProperty(PropertyName = "adContent")]
        public string AdContent { get; set; }

        [JsonProperty(PropertyName = "campaign")]
        public string Campaign { get; set; }

        [JsonProperty(PropertyName = "keyword")]
        public string Keyword { get; set; }

        [JsonProperty(PropertyName = "landingPage")]
        public string LandingPage { get; set; }

        [JsonProperty(PropertyName = "medium")]
        public string Medium { get; set; }

        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }
    }
}
