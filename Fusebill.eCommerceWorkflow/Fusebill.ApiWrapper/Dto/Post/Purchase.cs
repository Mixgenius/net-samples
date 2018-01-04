using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fusebill.ApiWrapper.Dto.Post
{
    public class Purchase
    {
        [JsonProperty(PropertyName = "customerId")]
        public long CustomerId { get; set; }

        [JsonProperty(PropertyName = "productId")]
        public long ProductId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
