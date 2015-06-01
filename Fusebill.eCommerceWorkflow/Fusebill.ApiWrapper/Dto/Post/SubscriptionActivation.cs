using Fusebill.ApiWrapper.Dto.Interfaces;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Post
{
    public class SubscriptionActivation : ISubscriptionBillingPeriodDefinition
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("billingPeriodId")]
        public long? BillingPeriodId { get; set; }

        [JsonProperty("invoiceMonth")]
        public int? InvoiceMonth{ get; set; }

        [JsonProperty("invoiceDay")]
        public int? InvoiceDay { get; set; }
    }
}
