using System.Collections.Generic;

namespace Model.Factories
{
    public interface IPlanOrderToCashCycleFactory
    {
        PlanOrderToCashCycle Create(PlanProduct planProduct, ProductType productType, PlanFrequency planFrequency, List<AccountCurrency> accountCurrencies);
        PlanOrderToCashCycle Create(PlanProduct planProduct, ProductType productType, PlanFrequency planFrequency, PricingModelType pricingModelType, ICollection<QuantityRange> quantityRanges, Interval? interval, int? numberOfIntervals);
        PlanOrderToCashCycle Clone(PlanFrequency newPlanFrequency, PlanProduct existingPlanProduct, PlanOrderToCashCycle existingOrderToCashCycle);
    }
}