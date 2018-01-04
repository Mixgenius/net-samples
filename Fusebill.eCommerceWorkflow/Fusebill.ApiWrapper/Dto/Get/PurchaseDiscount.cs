using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class PurchaseDiscount : BaseDto
    {
        [JsonProperty(PropertyName = "purchaseId")]
        public long PurchaseId { get; set; }

        [JsonProperty(PropertyName = "discountType")]
        [DisplayName("Discount Type")]
        public string DiscountType { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

    }
}
