using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class ProductItem : BaseDto
    {
        [JsonProperty(PropertyName = "purchaseId")]
        public long PurchaseId { get; set; }

        [JsonProperty(PropertyName = "reference")]
        [DisplayName("Product Item ID")]
        public string Reference { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}
