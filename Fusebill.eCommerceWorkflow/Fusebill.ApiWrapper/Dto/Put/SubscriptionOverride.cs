using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using Common.ValidationAttributes;
using Newtonsoft.Json;


namespace Fusebill.ApiWrapper.Dto.Put
{
    public class SubscriptionOverride : BaseDto
    {
        [JsonProperty(PropertyName = "name")]
        [StringLength(100, ErrorMessage="The name cannot exceed 100 characters")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "charge")]
     //   [Decimal6PlacesValidator]
        [Range(typeof(decimal), "0.00", "79228162514264337593543950335", ErrorMessage = "Amount must be greater than 0 and cannot exceed two decimal places")]
        public Nullable<decimal> Charge { get; set; }

        [JsonProperty(PropertyName = "setupFee")]
       // [Decimal6PlacesValidator]
        [Range(typeof(decimal), "0.00", "79228162514264337593543950335", ErrorMessage = "Amount must be greater than 0 and cannot exceed two decimal places")]
        public Nullable<decimal> SetupFee { get; set; }
    }
}
