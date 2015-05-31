using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Interfaces
{
    public interface ISubscriptionBillingPeriodDefinition
    {
        [JsonProperty(PropertyName = "billingPeriodId")]
        long? BillingPeriodId { get; set; }

        [JsonProperty(PropertyName = "invoiceDay")]
        int? InvoiceDay { get; set; }

        [JsonProperty(PropertyName = "invoiceMonth")]
        int? InvoiceMonth { get; set; }
    }
}
