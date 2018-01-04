using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class Product : BaseDto
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "productType")]
        public string ProductType { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "taxExempt")]
        public bool TaxExempt { get; set; }

        [JsonProperty(PropertyName = "orderToCashCycle")]
        public OrderToCashCycle OrderToCashCycle { get; set; }

        [JsonProperty(PropertyName = "availableForPurchase")]
        public bool AvailableForPurchase { get; set; }

        [JsonProperty(PropertyName = "isTrackingItems")]
        public bool IsTrackingItems { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public decimal? Quantity { get; set; }

        [JsonProperty(PropertyName = "avalaraItemCode")]
        public string AvalaraItemCode { get; set; }

        [JsonProperty(PropertyName = "avalaraTaxCode")]
        public string AvalaraTaxCode { get; set; }

        [JsonProperty(PropertyName = "glCode")]
        public string GlCode { get; set; }

        [JsonProperty(PropertyName = "deletable")]
        public bool Deletable { get; set; }

    }
}
