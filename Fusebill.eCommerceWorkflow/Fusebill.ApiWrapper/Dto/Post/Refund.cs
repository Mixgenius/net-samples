using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using Model.Internal;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Post
{
    public class Refund : BaseDto
    {
        [JsonProperty(PropertyName = "reference")]
        [StringLength(500, ErrorMessage = "Reference must not exceed 500 characters")]
        [DisplayName("Reference")]
        public string Reference { get; set; }

        [JsonProperty(PropertyName = "originalPaymentId")]
        [DisplayName("Original payment ID")]
        public long OriginalPaymentId { get; set; }

        [JsonProperty(PropertyName = "amount")]
        [Range(typeof (decimal), "0.01", "79228162514264337593543950335", ErrorMessage="Amount must be greater than 0 and cannot exceed two decimal places")]
        [RegularExpression(@"^\d*.?\d{0,2}?$", ErrorMessage = "Amount must be greater than 0 and cannot exceed two decimal places")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "refundAllocations")]
        public List<RefundAllocation> RefundAllocations { get; set; }

        //[JsonProperty(PropertyName = "method")]
        //[DisplayName("Refund Method")]
        //public RefundMethodOptions Method { get; set; }
    }
}
