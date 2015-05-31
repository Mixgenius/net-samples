using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using Common.ValidationAttributes;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Post
{
    public class Payment : BaseDto
    {
        [JsonProperty(PropertyName = "reference")]
        [StringLength(500, ErrorMessage = "Reference must not exceed 500 characters")]
        [DisplayName("Reference")]
        public string Reference { get; set; }

        [JsonProperty(PropertyName = "effectiveTimestamp")]
        [DisplayName("Effective timestamp")]
        public DateTime EffectiveTimestamp { get; set; }

        [JsonProperty(PropertyName = "customerId")]
        [DisplayName("Customer ID")]
        public long CustomerId { get; set; }

        [JsonProperty(PropertyName = "amount")]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "Amount must be greater than 0 and cannot exceed two decimal places")]
        [RegularExpression(@"^\d*.?\d{0,2}?$", ErrorMessage = "Amount must be greater than 0 and cannot exceed two decimal places")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "source")]
        [DisplayName("Source")]
    //    [PaymentSourceValidation]
        public string Source { get; set; }

        [JsonProperty(PropertyName = "paymentMethod")]
        [DisplayName("Payment Method")]
     //   [PaymentMethodTypeValidation]
        public string PaymentMethodType { get; set; }

        [JsonProperty(PropertyName = "paymentMethodId")]
        public Nullable<long> PaymentMethodTypeId { get; set; }

        [JsonProperty(PropertyName = "paymentAllocations")]
        public List<PaymentAllocation> PaymentAllocations { get; set; } 
    }
}
