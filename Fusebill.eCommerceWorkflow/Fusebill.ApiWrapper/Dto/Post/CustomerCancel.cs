//using Common.ValidationAttributes;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Fusebill.ApiWrapper.Dto.Post
{
    public class CustomerCancel : BaseDto
    {
        [DisplayName("Customer Id")]
        [JsonProperty(PropertyName = "customerId")]
        public long CustomerId { get; set; }

        [JsonProperty(PropertyName = "cancellationOption")]
        [DisplayName("Cancellation Option")]
  //      [CancelOptionValidationAttribute]
        public string CancellationOption { get; set; }
    }
}
