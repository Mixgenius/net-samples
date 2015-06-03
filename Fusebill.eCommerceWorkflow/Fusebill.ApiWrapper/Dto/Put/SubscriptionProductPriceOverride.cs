using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using Common.ValidationAttributes;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Put
{
    public class SubscriptionProductPriceOverride : BaseDto
    {
        [JsonProperty(PropertyName = "chargeAmount")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Amount must be a positive number and cannot exceed six decimal places")]
     //   [Decimal6PlacesValidator]
        [DisplayName("Charge")]
        public decimal? ChargeAmount { get; set; }
    }
}
