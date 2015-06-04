using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using Common.ValidationAttributes;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Put
{
    public class Price : BaseDto
    {
        [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Amount must be a positive number and cannot exceed six decimal places")]
     //   [Decimal6PlacesValidator]
        [DisplayName("Amount")]
        [JsonProperty(PropertyName = "amount")]
        public decimal? Amount { get; set; }
    
        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }
    }
}
