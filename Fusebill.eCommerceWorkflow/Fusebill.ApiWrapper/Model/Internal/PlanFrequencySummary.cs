using System.Collections.Generic;

namespace Model.Internal
{
    public class PlanFrequencySummary
    {
        public Interval Interval { get; set; }
        public int NumberOfIntervals { get; set; }
        public PricingModelType PricingModelType { get; set; }
        public IEnumerable<QuantityRange> QuantityRanges { get; set; }
    }
}
