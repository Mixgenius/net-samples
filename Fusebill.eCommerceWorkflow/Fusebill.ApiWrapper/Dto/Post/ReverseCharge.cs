using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using Common.ValidationAttributes;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Post
{
    public class ReverseCharge : BaseDto
    {
        [JsonProperty(PropertyName = "chargeId")]
        [DisplayName("Charge Id")]
        public long ChargeId { get; set; }

        [JsonProperty(PropertyName = "reverseChargeOption")]
        [DisplayName("Reverse charge option")]
       // [ReverseChargeOptionValidation]
        public string ReverseChargeOption { get; set; }

        [JsonProperty(PropertyName = "reverseChargeAmount")]
        [DisplayName("Reverse charge amount")]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "Amount must be greater than 0 and cannot exceed two decimal places")]
        [RegularExpression(@"^\d*.?\d{0,2}?$", ErrorMessage = "Amount must be greater than 0 and cannot exceed two decimal places")]
        public decimal? ReverseChargeAmount { get; set; }

        [JsonProperty(PropertyName = "reference")]
        [DisplayName("Reference")]
        [StringLength(500)]
        public string Reference { get; set; }
    }
}
