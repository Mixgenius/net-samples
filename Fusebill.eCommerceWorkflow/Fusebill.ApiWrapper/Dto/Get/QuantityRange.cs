using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class QuantityRange : BaseDto
    {
        [JsonProperty(PropertyName = "min")]
        public decimal Min { get; set; }
    
        [JsonProperty(PropertyName = "max")]
        public decimal? Max { get; set; }

        [JsonProperty(PropertyName = "prices")]
        public List<Price> Prices { get; set; }
    }
}
