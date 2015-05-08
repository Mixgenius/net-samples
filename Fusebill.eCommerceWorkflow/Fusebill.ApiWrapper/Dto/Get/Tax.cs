using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class Tax
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "registrationCode")]
        public string RegistrationCode { get; set; }

        [JsonProperty(PropertyName = "percent")]
        public decimal Percent { get; set; }

        [JsonProperty(PropertyName = "total")]
        public decimal Total { get; set; }
    }
}
