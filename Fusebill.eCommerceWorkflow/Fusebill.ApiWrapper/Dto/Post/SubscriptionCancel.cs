using System.ComponentModel;
//using Common.ValidationAttributes;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Post
{
    public class SubscriptionCancel : BaseDto
    {
        [JsonProperty(PropertyName = "subscriptionId")]
        [DisplayName("Subscription Id")]
        public long SubscriptionId { get; set; }

        [JsonProperty(PropertyName = "cancellationOption")]
        [DisplayName("Cancellation Option")]
    //    [CancelOptionValidation]
        public string CancellationOption { get; set; }
    }
}
