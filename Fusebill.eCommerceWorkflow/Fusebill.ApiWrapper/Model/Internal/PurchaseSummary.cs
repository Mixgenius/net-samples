using System;
using System.Collections.Generic;

namespace Model.Internal
{
    public class PurchaseSummary
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public decimal Quantity { get; set; }

        public PurchaseStatus Status { get; set; }

        public PricingModelType PricingModelType { get; set; }

        public decimal Amount { get; set; }

        public decimal TaxableAmount { get; set; }

        public ICollection<PurchasePriceRange> PurchasePriceRanges { get; set; }

        public DateTime EffectiveTimestamp { get; set; }
    }
}
