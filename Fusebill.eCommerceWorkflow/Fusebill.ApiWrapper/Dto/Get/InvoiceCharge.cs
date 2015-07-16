using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class InvoiceCharge : BaseDto
    {
        [JsonProperty(PropertyName = "quantity")]
        [DisplayName("Quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty(PropertyName = "unitPrice")]
        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }

        [JsonProperty(PropertyName = "amount")]
        [DisplayName("Amount")]
        public decimal Amount { get; set; }    
        
        [JsonProperty(PropertyName = "name")]
        [DisplayName("Name")]
        public string Name { get; set; }
        
        [JsonProperty(PropertyName = "description")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "glCode")]
        [DisplayName("GL Code")]
        public string GLCode { get; set; }

        [JsonProperty(PropertyName = "effectiveTimestamp")]
        public DateTime EffectiveTimestamp { get; set; }

        [JsonProperty(PropertyName = "proratedUnitPrice")]
        [DisplayName("Prorated Unit Price")]
        public decimal? ProratedUnitPrice { get; set; }

        [JsonProperty(PropertyName = "startServiceDate")]
        public DateTime StartServiceDate { get; set; }

        [JsonProperty(PropertyName = "endServiceDate")]
        public DateTime EndServiceDate { get; set; }

        [JsonProperty(PropertyName = "rangeQuantity")]
        public decimal? RangeQuantity { get; set; }

        [JsonProperty(PropertyName = "isReversable")]
        public bool IsReversable { get; set; }

        //[JsonProperty(PropertyName = "discount")]
        //public Discount Discount { get; set; }

        //[JsonProperty(PropertyName = "discounts")]
        //public List<Discount> Discounts { get; set; }

        //[JsonProperty(PropertyName = "subscriptionProduct")]
        //public SubscriptionProductCharge SubscriptionProduct { get; set; }

        //[JsonProperty(PropertyName = "purchase")]
        //public PurchaseCharge Purchase { get; set; }
    }
}
