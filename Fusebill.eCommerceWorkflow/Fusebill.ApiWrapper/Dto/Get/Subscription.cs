using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class Subscription : BaseDto
    {
        [JsonProperty(PropertyName = "customerId")]
        public long CustomerId { get; set; }

        [JsonProperty(PropertyName = "planFrequency")]
        public PlanFrequency PlanFrequency { get; set; }

        [JsonProperty(PropertyName = "planCode")]
        public string PlanCode { get; set; }

        [JsonProperty(PropertyName = "planName")]
        public string PlanName { get; set; }

        [JsonProperty(PropertyName = "planDescription")]
        public string PlanDescription { get; set; }

        [JsonProperty(PropertyName = "planReference")]
        public string PlanReference { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "reference")]
        public string Reference { get; set; }
        
        [JsonProperty(PropertyName = "subscriptionOverride")]
        public SubscriptionOverride SubscriptionOverride { get; set; }

        [JsonProperty(PropertyName = "hasPostedInvoice")]
        public bool HasPostedInvoice { get; set; }

        [JsonProperty(PropertyName = "createdTimestamp")]
        [DisplayName("Created")]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "activatedTimestamp")]
        [DisplayName("Activated")]
        public DateTime? Activated { get; set; }

        [JsonProperty(PropertyName = "provisionedTimestamp")]
        [DisplayName("Provisioned")]
        public DateTime? ProvisionedTimestamp { get; set; }

        [JsonProperty(PropertyName = "nextPeriodStartDate")]
        [DisplayName("Next Recharge")]
        public DateTime? NextPeriodStartDate { get; set; }

        [JsonProperty(PropertyName = "scheduledActivationTimestamp")]
        [DisplayName("Scheduled")]
        public DateTime? ScheduledActivationTimestamp { get; set; }

        [JsonProperty(PropertyName = "subscriptionProducts")]
        public List<SubscriptionProduct> SubscriptionProducts { get; set; }

        [JsonProperty(PropertyName = "remainingInterval")]
        public int? RemainingInterval { get; set; }

        [JsonProperty(PropertyName = "openSubscriptionPeriodEndDate")]
        public DateTime? OpenSubscriptionPeriodEndDate { get; set; }

        [JsonProperty(PropertyName = "invoicePreview")]
        public InvoicePreview InvoicePreview { get; set; }

        public bool ShouldSerializeInvoicePreview() { return InvoicePreview != null; }

        [JsonProperty(PropertyName = "additionalInvoicesForPreview")]
        public List<InvoicePreview> AdditionalInvoicesForPreview { get; set; }

        public bool ShouldSerializeAdditionalInvoicesForPreview() { return AdditionalInvoicesForPreview != null; }

        [JsonProperty(PropertyName = "chargeDiscount")]
        public SubscriptionProductDiscount ChargeDiscount { get; set; }

        [JsonProperty(PropertyName = "setupFeeDiscount")]
        public SubscriptionProductDiscount SetupFeeDiscount { get; set; }

        [JsonProperty(PropertyName = "chargeDiscounts")]
        public List<SubscriptionProductDiscount> ChargeDiscounts { get; set; }

        [JsonProperty(PropertyName = "setupFeeDiscounts")]
        public List<SubscriptionProductDiscount> SetupFeeDiscounts { get; set; }

        [JsonProperty(PropertyName = "customFields")]
        public List<SubscriptionCustomField> CustomFields { get; set; }

        [JsonProperty(PropertyName = "planAutoApplyChanges")]
        public bool PlanAutoApplyChanges { get; set; }

        [JsonProperty(PropertyName = "autoApplyCatalogChanges")]
        public bool AutoApplyCatalogChanges { get; set; }

        [JsonProperty(PropertyName = "monthlyRecurringRevenue")]
        [DisplayName("Monthly Recurring Revenue")]
        public decimal MonthlyRecurringRevenue { get; set; }

        [JsonProperty(PropertyName = "netMonthlyRecurringRevenue")]
        [DisplayName("Net MRR")]
        public decimal NetMonthlyRecurringRevenue { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "contractStartTimestamp")]
        [DisplayName("Contract Start Date")]
        public DateTime? ContractStartTimestamp { get; set; }

        [JsonProperty(PropertyName = "contractEndTimestamp")]
        [DisplayName("Contract End Date")]
        public DateTime? ContractEndTimestamp { get; set; }

        [JsonProperty(PropertyName = "coupons")]
        public List<SubscriptionCoupon> Coupons { get; set; }
    }
}
