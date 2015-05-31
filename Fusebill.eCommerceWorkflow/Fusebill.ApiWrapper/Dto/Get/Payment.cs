using System.Collections.Generic;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class Payment : BaseDto
    {
        [JsonProperty(PropertyName = "paymentActivityId")]
        public long PaymentActivityId { get; set; }

        [JsonProperty(PropertyName = "reference")]
        public string Reference { get; set; }

        [JsonProperty(PropertyName = "effectiveTimestamp")]
        public System.DateTime EffectiveTimestamp { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "customerId")]
        public long CustomerId { get; set; }

        //Nullable because payments don't have an original payment activity
        [JsonProperty(PropertyName = "originalPaymentActivityId")]
        public long? OriginalPaymentActivityId { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "invoiceAllocations")]
        public List<InvoiceAllocation> InvoiceAllocations { get; set; }

        [JsonProperty(PropertyName = "refunds")]
        public List<Payment> Refunds { get; set; }

        public bool ShouldSerializeRefunds()
        {
            return Refunds != null;
        }

        [JsonProperty(PropertyName = "unallocatedAmount")]
        public decimal UnallocatedAmount { get; set; }

        //Nullable because refunds don't have a refundable amount
        [JsonProperty(PropertyName = "refundableAmount")]
        public decimal? RefundableAmount { get; set; }

        public bool ShouldSerializeRefundableAmount()
        {
            return RefundableAmount.HasValue;
        }
    }
}