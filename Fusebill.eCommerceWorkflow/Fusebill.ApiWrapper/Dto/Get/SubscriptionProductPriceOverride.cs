using System.ComponentModel;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class SubscriptionProductPriceOverride : BaseDto
    {
        [JsonProperty(PropertyName = "chargeAmount")]
        [DisplayName("Charge")]
        public decimal ChargeAmount { get; set; }
    }
}
