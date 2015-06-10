using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using Common.ValidationAttributes;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Interfaces
{
    public interface ISubscriptionProductDiscount
    {
        [JsonProperty(PropertyName = "discountType")]
        [DisplayName("Discount Type")]
        [Required]
        string DiscountType { get; set; }

        [JsonProperty(PropertyName = "amount")]
        [DisplayName("Amount")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Amount must be a positive number and cannot exceed six decimal places")]
        decimal Amount { get; set; }

        [JsonProperty(PropertyName = "remainingUsagesUntilStart")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of periods must be greater than 0")]
        int RemainingUsagesUntilStart { get; set; }

        [JsonProperty(PropertyName = "remainingUsage")]
        int? RemainingUsage { get; set; }
    }
}
