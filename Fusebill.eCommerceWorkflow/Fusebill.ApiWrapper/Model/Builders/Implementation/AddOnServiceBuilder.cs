namespace Model.Builders.Implementation
{
    public class RecurringServiceBuilder : ProductTypeBuilder
    {
        public void BuildQuantityChangeChargeModel(PlanOrderToCashCycle orderToCashCycle)
        {
            orderToCashCycle.QuantityProrateNegativeQuantity = false;
            orderToCashCycle.QuantityProratePositiveQuantity = false;
            orderToCashCycle.QuantityReverseChargeNegativeQuantity = false;
            orderToCashCycle.QuantityChargeTimingType = ChargeTimingType.StartOfPeriod;
        }

        public void BuildRecurringChargeModel(PlanOrderToCashCycle orderToCashCycle)
        {
            orderToCashCycle.RecurProrateNegativeQuantity = false;
            orderToCashCycle.RecurProratePositiveQuantity = false;
            orderToCashCycle.RecurReverseChargeNegativeQuantity = false;
            orderToCashCycle.RecurChargeTimingType = ChargeTimingType.StartOfPeriod;
        }

        public void BuildRevenueRecognitionModel(OrderToCashCycle orderToCashCycle, Interval? interval, int? numberOfIntervals)
        {
            if (null == interval || null == numberOfIntervals)
            {
                orderToCashCycle.IsEarnedImmediately = true;
                orderToCashCycle.EarningTimingInterval = EarningTimingInterval.EarnImmediately;
                orderToCashCycle.EarningTimingType = EarningTimingType.StartOfInterval;
            }
            else
            {
                orderToCashCycle.IsEarnedImmediately = false;
                orderToCashCycle.EarningInterval = interval;
                orderToCashCycle.EarningNumberOfInterval = numberOfIntervals;
                orderToCashCycle.EarningTimingInterval = EarningTimingInterval.Daily;
                orderToCashCycle.EarningTimingType = EarningTimingType.EndOfInterval;
            }
        }
    }
}
