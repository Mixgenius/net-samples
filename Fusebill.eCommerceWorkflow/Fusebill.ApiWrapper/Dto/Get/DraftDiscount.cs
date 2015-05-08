using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class DraftDiscount : BaseDto
    {
        [JsonProperty(PropertyName = "draftChargeId")]
        [DisplayName("Draft Charge ID")]
        public long DraftChargeId { get; set; }

        [JsonProperty(PropertyName = "configuredDiscountAmount")]
        [DisplayName("Configured Discount Amount")]
        public decimal ConfiguredDiscountAmount { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "discountType")]
        [DisplayName("Discount Type")]
        public string DiscountType { get; set; }

        [JsonProperty(PropertyName = "effectiveTimestamp")]
        public DateTime EffectiveTimestamp { get; set; }

        [JsonProperty(PropertyName = "description")]
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}
