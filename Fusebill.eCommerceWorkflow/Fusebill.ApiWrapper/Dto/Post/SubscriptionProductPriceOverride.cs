using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using Fusebill.ApiWrapper.ValidationAttributes;
using Newtonsoft.Json;
using Fusebill.ApiWrapper.Dto.Interfaces;


namespace Fusebill.ApiWrapper.Dto.Post
{
    public class SubscriptionProductPriceOverrideLite : BaseDto, ISubscriptionProductPriceOverride
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "chargeAmount")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Amount must be a positive number and cannot exceed six decimal places")]
      //  [Decimal6PlacesValidator]
        [DisplayName("Charge")]
        public decimal ChargeAmount { get; set; }
    }
}
