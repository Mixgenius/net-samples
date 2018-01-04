using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fusebill.ApiWrapper.Dto.Patch
{
    public class SubscriptionProductItems
    {
        [JsonProperty(PropertyName = "subscriptionId")]
        public long SubscriptionId { get; set; }

        [JsonProperty(PropertyName = "subscriptionProducts")]
        public List<SubscriptionProduct> SubscriptionProducts { get; set; }

    }

    public class SubscriptionProduct
    {
        [JsonProperty(PropertyName = "subscriptionProductId")]
        public long SubscriptionProductId { get; set; }


        [JsonProperty(PropertyName = "subscriptionProductItems")]
        public List<SubscriptionProductItem> SubscriptionProductItems { get; set; }
    }

    public class SubscriptionProductItem
    { 
        [JsonProperty(PropertyName = "reference")]
        public string Reference { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Update, Delete or Insert
        /// </summary>
        [JsonProperty(PropertyName = "operation")]
        public string Operation { get; set; }
    }
}
