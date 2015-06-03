using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Fusebill.ApiWrapper.Dto.Interfaces;
//using Fusebill.ApiWrapper.ValidationAttributes;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Put
{
    public class Subscription : BaseDto, ISubscriptionBillingPeriodDefinition, IValidatableObject
    {
        public Subscription()
        {
            ChargeDiscounts = new List<SubscriptionProductDiscount>();
            SetupFeeDiscounts = new List<SubscriptionProductDiscount>();
        }

        [JsonProperty(PropertyName = "subscriptionOverride")]
        public SubscriptionOverride SubscriptionOverride { get; set; }

     //   [SubscriptionStatusValidation]
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "reference")]
        [StringLength(255, ErrorMessage = "Reference must be less than 255 characters")]
        public string Reference { get; set; }

        [JsonProperty(PropertyName = "subscriptionProducts")]
        public List<SubscriptionProduct> SubscriptionProducts { get; set; }

        [JsonProperty(PropertyName = "scheduledActivationTimestamp")]
        [DisplayName("Scheduled")]
        public DateTime? ScheduledActivationTimestamp { get; set; }

        [JsonProperty(PropertyName = "remainingInterval")]
        [DisplayName("Remaining intervals")]
        public int? RemainingInterval { get; set; }

        [JsonProperty(PropertyName = "openSubscriptionPeriodEndDate")]
        public DateTime? OpenSubscriptionPeriodEndDate { get; set; }

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

        [JsonProperty(PropertyName = "billingPeriodId")]
        public long? BillingPeriodId { get; set; }

        [JsonProperty(PropertyName = "invoiceDay")]
        public int? InvoiceDay { get; set; }

        [JsonProperty(PropertyName = "invoiceMonth")]
        public int? InvoiceMonth { get; set; }

        [JsonProperty(PropertyName = "contractStartTimestamp")]
        [DisplayName("Contract Start Date")]
        public DateTime? ContractStartTimestamp { get; set; }

        [JsonProperty(PropertyName = "contractEndTimestamp")]
        [DisplayName("Contract End Date")]
        public DateTime? ContractEndTimestamp { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ChargeDiscount != null && ChargeDiscounts != null && ChargeDiscounts.Any())
                yield return new ValidationResult("Please specify either a single charge discount or the collection of charge discounts.", new[] { "ChargeDiscount", "ChargeDiscounts" });

            if (SetupFeeDiscount != null && SetupFeeDiscounts != null && SetupFeeDiscounts.Any())
                yield return new ValidationResult("Please specify either a single setup fee discount or the collection of setup fee discounts.", new[] { "SetupFeeDiscount", "SetupFeeDiscounts" });
        }
    }
}
