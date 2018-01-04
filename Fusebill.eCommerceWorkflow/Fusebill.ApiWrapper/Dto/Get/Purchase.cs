using Newtonsoft.Json;
using System.Collections.Generic;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class Purchase : BaseDto
    {
        [JsonProperty(PropertyName = "customerId")]
        public long CustomerId { get; set; }


        [JsonProperty(PropertyName = "productId")]
        public long ProductId { get; set; }


        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }


        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }


        [JsonProperty(PropertyName = "isTrackingItems")]
        public bool IsTrackingItems { get; set; }


        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }


        [JsonProperty(PropertyName = "quantity")]
        public long Quantity { get; set; }


        [JsonProperty(PropertyName = "customFields")]
        public List<CustomField> customFields { get; set; }


        [JsonProperty(PropertyName = "discounts")]
        public List<PurchaseDiscount> Discounts { get; set; }


        [JsonProperty(PropertyName = "priceRanges")]
        public List<PriceRange> PriceRanges { get; set; }


        [JsonProperty(PropertyName = "productItems")]
        public List<ProductItem> ProductItems { get; set; }


        [JsonProperty(PropertyName = "couponCodes")]
        public List<string> CouponCodes { get; set; }

    }
}
