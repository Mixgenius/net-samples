using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class Price : BaseDto
    {
        [JsonProperty(PropertyName = "amount")]
        public decimal? Amount { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }
    }
}
