namespace Model.Builders
{
    public interface ProductTypeBuilder
    {
        void BuildQuantityChangeChargeModel(PlanOrderToCashCycle orderToCashCycle);
        void BuildRecurringChargeModel(PlanOrderToCashCycle orderToCashCycle);
        void BuildRevenueRecognitionModel(OrderToCashCycle orderToCashCycle, Interval? interval, int? numberOfIntervals);
    }
}
