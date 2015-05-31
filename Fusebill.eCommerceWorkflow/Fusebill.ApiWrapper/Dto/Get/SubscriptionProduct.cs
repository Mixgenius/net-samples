using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class SubscriptionProduct : BaseDto
    {
        [JsonProperty(PropertyName = "subscriptionId")]
        public long SubscriptionId { get; set; }

        [JsonProperty(PropertyName = "planProduct")]
        public PlanProduct PlanProduct { get; set; }

        [DisplayName("Quantity")]
        [JsonProperty(PropertyName = "quantity")]
        public decimal Quantity { get; set; }

        [DisplayName("Is Included")]
        [JsonProperty(PropertyName = "isIncluded")]
        public bool IsIncluded { get; set; }

        [DisplayName("Scheduled Start Date")]
        [JsonProperty(PropertyName = "startDate")]
        public DateTime? StartDate { get; set; }

        [JsonProperty(PropertyName = "subscriptionProductOverride")]
        public SubscriptionProductOverride SubscriptionProductOverride { get; set; }

        [JsonProperty(PropertyName = "subscriptionProductPriceOverride")]
        public SubscriptionProductPriceOverride SubscriptionProductPriceOverride { get; set; }

        [JsonProperty(PropertyName = "chargeAtSubscriptionActivation")]
        public bool ChargeAtSubscriptionActivation { get; set; }

        [JsonProperty(PropertyName = "isCharged")]
        public bool IsCharged { get; set; }

        [JsonProperty(PropertyName = "subscriptionProductDiscount")]
        public SubscriptionProductDiscount SubscriptionProductDiscount { get; set; }

        [JsonProperty(PropertyName = "subscriptionProductDiscounts")]
        public List<SubscriptionProductDiscount> SubscriptionProductDiscounts { get; set; }

        [JsonProperty(PropertyName = "customFields")]
        public List<SubscriptionProductCustomField> SubscriptionProductCustomFields { get; set; }

        [JsonProperty(PropertyName = "monthlyRecurringRevenue")]
        public decimal MonthlyRecurringRevenue { get; set; }

        [JsonProperty(PropertyName = "netMonthlyRecurringRevenue")]
        [DisplayName("Net MRR")]
        public decimal NetMonthlyRecurringRevenue { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "invoicePreview")]
        public InvoicePreview InvoicePreview { get; set; }

        public bool ShouldSerializeInvoicePreview() { return InvoicePreview != null; }

        [JsonProperty(PropertyName = "lastPurchaseDate")]
        public DateTime? LastPurchaseDate { get; set; }

        [JsonProperty(PropertyName = "earningSettings")]
        public SubscriptionProductEarningTiming EarningSettings { get; set; }
    }
}
