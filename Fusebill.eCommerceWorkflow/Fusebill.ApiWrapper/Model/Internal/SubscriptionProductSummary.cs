using System;

namespace Model.Internal
{
    public class SubscriptionProductSummary : Entity
    {
        public long CustomerId { get; set; }
        public long SubscriptionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public bool Included { get; set; }
        public byte SortOrder { get; set; }
        public ProductType ProductType { get; set; }
        public decimal Amount { get; set; }
        public PricingModelOverride PricingModelOverride { get; set; }
        public PlanFrequencySummary PlanFrequencySummary { get; set; }
        public ProductResetType ResetType { get; set; }
        public bool TrackingItems { get; set; }
    }
}
