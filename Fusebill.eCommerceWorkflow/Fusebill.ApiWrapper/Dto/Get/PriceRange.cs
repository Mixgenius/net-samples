using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class PriceRange : BaseDto
    {
        [JsonProperty(PropertyName = "min")]
        public decimal Min { get; set; }

        [JsonProperty(PropertyName = "max")]
        public decimal? Max { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }
    }
}
