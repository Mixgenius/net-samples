using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
// using Common.Dto.Interfaces;
using Fusebill.ApiWrapper.Dto.Interfaces;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Post
{
    public class Subscription : BaseDto, ISubscriptionBillingPeriodDefinition, IValidatableObject
    {
        [JsonProperty(PropertyName = "customerId")]
        [Range(1, long.MaxValue, ErrorMessage="Please specify a value for customerId")]
        public long CustomerId { get; set; }

        [JsonProperty(PropertyName = "planFrequencyId")]
        [Range(1, long.MaxValue, ErrorMessage = "Please specify a value for planFrequencyId")]
        public long PlanFrequencyId { get; set; }

        [JsonProperty(PropertyName = "subscriptionOverride")]
        public SubscriptionOverride SubscriptionOverride { get; set; }

        [JsonProperty(PropertyName = "scheduledActivationTimestamp")]
        [DisplayName("Scheduled")]
        public DateTime? ScheduledActivationTimestamp { get; set; }

        [JsonProperty(PropertyName = "remainingInterval")]
        [DisplayName("Remaining intervals")]
        public int? RemainingInterval { get; set; }

        [JsonProperty(PropertyName = "reference")]
        [StringLength(255, ErrorMessage = "Reference must be less than 255 characters")]
        public string Reference { get; set; }

        [JsonProperty(PropertyName = "chargeDiscount")]
        public SubscriptionProductDiscount ChargeDiscount { get; set; }

        [JsonProperty(PropertyName = "setupFeeDiscount")]
        public SubscriptionProductDiscount SetupFeeDiscount { get; set; }

        [JsonProperty(PropertyName = "chargeDiscounts")]
        public List<SubscriptionProductDiscount> ChargeDiscounts { get; set; }

        [JsonProperty(PropertyName = "setupFeeDiscounts")]
        public List<SubscriptionProductDiscount> SetupFeeDiscounts { get; set; }

        [JsonProperty(PropertyName = "autoApplyCatalogChanges")]
        public bool? AutoApplyCatalogChanges { get; set; }

        [JsonProperty(PropertyName = "contractStartTimestamp")]
        [DisplayName("Contract Start Date")]
        public DateTime? ContractStartTimestamp { get; set; }

        [JsonProperty(PropertyName = "contractEndTimestamp")]
        [DisplayName("Contract End Date")]
        public DateTime? ContractEndTimestamp { get; set; }

        [JsonProperty(PropertyName = "billingPeriodId")]
        public long? BillingPeriodId { get; set; }

        [JsonProperty(PropertyName = "invoiceDay")]
        public int? InvoiceDay { get; set; }

        [JsonProperty(PropertyName = "invoiceMonth")]
        public int? InvoiceMonth { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ChargeDiscount != null && ChargeDiscounts != null && ChargeDiscounts.Any())
                yield return new ValidationResult("Please specify either a single charge discount or the collection of charge discounts.", new[] { "ChargeDiscount", "ChargeDiscounts" });

            if (SetupFeeDiscount != null && SetupFeeDiscounts != null && SetupFeeDiscounts.Any())
                yield return new ValidationResult("Please specify either a single setup fee discount or the collection of setup fee discounts.", new[] { "SetupFeeDiscount", "SetupFeeDiscounts" });
        }
    }
}
