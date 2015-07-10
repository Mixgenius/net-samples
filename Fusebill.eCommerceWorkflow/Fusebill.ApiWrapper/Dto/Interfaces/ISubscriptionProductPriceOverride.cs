using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using Fusebill.ApirWrapper.ValidationAttributes;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Interfaces
{
    public interface ISubscriptionProductPriceOverride
    {
        [JsonProperty("id")]
        long Id { get; set; }

        [JsonProperty(PropertyName = "chargeAmount")]
        [Range(typeof (decimal), "0", "79228162514264337593543950335", ErrorMessage = "Amount must be a positive number and cannot exceed six decimal places")]
      //  [Decimal6PlacesValidator]
        [DisplayName("Charge")]
        decimal ChargeAmount { get; set; }
    }
}