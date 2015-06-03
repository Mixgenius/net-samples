using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Fusebill.ApiWrapper.Dto.Put
{
    public class SubscriptionProduct : BaseDto, IValidatableObject
    {
        public SubscriptionProduct()
        {
            SubscriptionProductDiscounts = new List<SubscriptionProductDiscount>();
        }

        [JsonProperty(PropertyName = "planProductId")]
        public long PlanProductId { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        [Range(0, 2147483647, ErrorMessage = "Quantity must be a positive number or 0")]
        public decimal Quantity { get; set; }

        [JsonProperty(PropertyName = "isIncluded")]
        public bool IsIncluded { get; set; }

        [JsonProperty(PropertyName = "startDate")]
        public DateTime? StartDate { get; set; }

        [JsonProperty(PropertyName = "subscriptionProductOverride")]
        public SubscriptionProductOverride SubscriptionProductOverride { get; set; }

        [JsonProperty(PropertyName = "subscriptionProductPriceOverride")]
        public SubscriptionProductPriceOverride SubscriptionProductPriceOverride { get; set; }

        [JsonProperty(PropertyName = "chargeAtSubscriptionActivation")]
        public bool ChargeAtSubscriptionActivation { get; set; }

        [JsonProperty(PropertyName = "subscriptionProductDiscount")]
        public SubscriptionProductDiscount SubscriptionProductDiscount { get; set; }

        [JsonProperty(PropertyName = "subscriptionProductDiscounts")]
        public List<SubscriptionProductDiscount> SubscriptionProductDiscounts { get; set; }

        [JsonProperty(PropertyName = "quantityChangeDescription")]
        [StringLength(1000)]
        public string QuantityChangeDescription { get; set; }

        [JsonProperty(PropertyName = "earningSettings")]
        public SubscriptionProductEarningTiming EarningSettings { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SubscriptionProductDiscount != null && SubscriptionProductDiscounts != null && SubscriptionProductDiscounts.Any())
                yield return new ValidationResult("Please specify either a single discount or the collection of discounts.", new[] { "SubscriptionProductDiscount", "SubscriptionProductDiscounts" });
        }
    }
}
