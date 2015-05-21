using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class CustomerReference : BaseDto
    {
        [JsonProperty(PropertyName = "reference1")]
        public string Reference1 { get; set; }

        [JsonProperty(PropertyName = "reference2")]
        public string Reference2 { get; set; }

        [JsonProperty(PropertyName = "reference3")]
        public string Reference3 { get; set; }

        [DisplayName("Classic Id")]
        [JsonProperty(PropertyName = "classicId")]
        public long? ClassicId { get; set; }

        [JsonProperty(PropertyName = "salesTrackingCodes")]
        public List<CustomerSalesTrackingCode> SalesTrackingCodes { get; set; }

        public bool ShouldSerializeClassicId()
        {
            return ClassicId.HasValue;
        }
    }
}
