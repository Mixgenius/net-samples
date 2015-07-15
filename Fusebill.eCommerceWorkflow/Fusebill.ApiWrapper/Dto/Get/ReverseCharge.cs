using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using Common.ValidationAttributes;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class ReverseCharge : BaseDto
    {
        [JsonProperty(PropertyName = "reference")]
        [StringLength(500, ErrorMessage = "Reference must not exceed 500 characters")]
        [DisplayName("Reference")]
        public string Reference { get; set; }
        
        [JsonProperty(PropertyName = "amount")]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "Amount must be greater than 0 and cannot exceed two decimal places")]
      //  [Decimal6PlacesValidator]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "originalChargeId")]
        [DisplayName("Original Charge ID")]
      //  [PaymentSourceValidation]
        public long OriginalChargeId { get; set; }
    }
}
