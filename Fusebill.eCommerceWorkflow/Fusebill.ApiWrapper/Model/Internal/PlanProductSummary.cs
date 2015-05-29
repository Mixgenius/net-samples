using System.Collections.Generic;

namespace Model.Internal
{
    using System;

    public class PlanProductSummary : Entity
    {
        public long PlanId { get; set; }
        public long PlanRevisionId { get; set; }
        public bool IsOptional { get; set; }
        public bool IsIncludedByDefault { get; set; }
        public decimal Quantity { get; set; }
        public bool IsTrackingItems { get; set; }
        public ProductResetType ResetType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ProductType ProductType { get; set; }
        public long AccountId { get; set; }
        public PlanProductStatus Status { get; set; }
        public Nullable<long> PlanProductUniqueId { get; set; }
        public long ProductId { get; set; }
        public List<PlanFrequencySummary> Frequencies { get; set; } 
    }
}