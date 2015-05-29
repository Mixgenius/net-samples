using System.Collections.Generic;
using System.Linq;
using DataCommon.Models;
using Model.Builders;

namespace Model.Factories.Implementation
{
    public class PlanOrderToCashCycleFactory : IPlanOrderToCashCycleFactory
    {
        private readonly Dictionary<ProductType, ProductTypeBuilder> _builders;
        
        public PlanOrderToCashCycleFactory(Dictionary<ProductType, ProductTypeBuilder> builders)
        {
            _builders = builders;
        }

        public PlanOrderToCashCycle Create(PlanProduct planProduct, ProductType productType, PlanFrequency planFrequency, List<AccountCurrency> accountCurrencies)
        {
            var quantityRanges = new List<QuantityRange>
            {
                new QuantityRange
                {
                    Prices = accountCurrencies.Select(ac => new Price{ CurrencyId = ac.CurrencyId }).ToList()
                }
            };

            ValidateNotNull(planProduct, planFrequency, quantityRanges);

            var orderToCashCycle = new PlanOrderToCashCycle
            {
                PricingModelType = PricingModelType.Standard,
                QuantityRanges = quantityRanges
            };

            SetupOrderToCashCycle(planProduct, productType, planFrequency, orderToCashCycle);

            return orderToCashCycle;
        }

        public PlanOrderToCashCycle Create(PlanProduct planProduct, ProductType productType, PlanFrequency planFrequency, PricingModelType pricingModelType, ICollection<QuantityRange> quantityRanges, Interval? earningInterval, int? earningNumberOfIntervals)
        {
            ValidateNotNull(planProduct, planFrequency, quantityRanges);

            var orderToCashCycle = new PlanOrderToCashCycle
            {
                PricingModelType = pricingModelType,
                QuantityRanges = quantityRanges
            };

            SetupOrderToCashCycle(planProduct, productType, planFrequency, orderToCashCycle);

            return orderToCashCycle;
        }

        private void SetupOrderToCashCycle(PlanProduct planProduct, ProductType productType, PlanFrequency planFrequency, PlanOrderToCashCycle orderToCashCycle)
        {
            ProductTypeBuilder builder = _builders[productType];
            builder.BuildQuantityChangeChargeModel(orderToCashCycle);
            builder.BuildRecurringChargeModel(orderToCashCycle);

            if (productType == ProductType.PhysicalGood || productType == ProductType.PlanSetupFee)
                builder.BuildRevenueRecognitionModel(orderToCashCycle, null, null);

            if (productType == ProductType.RecurringService || productType == ProductType.PlanCharge)
                builder.BuildRevenueRecognitionModel(orderToCashCycle, planFrequency.Interval, planFrequency.NumberOfIntervals);

            if (productType == ProductType.OneTimeCharge)
                builder.BuildRevenueRecognitionModel(orderToCashCycle, null, null);

            planProduct.PlanOrderToCashCycles.Add(orderToCashCycle);
            planFrequency.PlanOrderToCashCycles.Add(orderToCashCycle);

            orderToCashCycle.PlanProduct = planProduct;
            orderToCashCycle.PlanFrequency = planFrequency;
            orderToCashCycle.PlanProductId = planProduct.Id;
            orderToCashCycle.PlanFrequencyId = planFrequency.Id;
        }

        private static void ValidateNotNull(PlanProduct planProduct, PlanFrequency planFrequency, ICollection<QuantityRange> quantityRanges)
        {
            if (planProduct == null) throw new FusebillException("You must provide a planProduct");
            if (planFrequency == null) throw new FusebillException("You must provide a planFrequency");
            if (null == quantityRanges || quantityRanges.Count == 0)
                throw new FusebillException("You must provide at least one QuantityRange for PricingModel.");
        }

        public PlanOrderToCashCycle Clone(PlanFrequency newPlanFrequency, PlanProduct existingPlanProduct, PlanOrderToCashCycle existingOrderToCashCycle)
        {
            var orderToCashCycle = new PlanOrderToCashCycle
            {
                PlanFrequency = newPlanFrequency,
                PlanProductId = existingPlanProduct.Id,
                PricingModelType = existingOrderToCashCycle.PricingModelType,
                EarningInterval = existingOrderToCashCycle.IsEarnedImmediately ? default(Interval?) : newPlanFrequency.Interval,
                EarningNumberOfInterval = existingOrderToCashCycle.IsEarnedImmediately ? new int?() : newPlanFrequency.NumberOfIntervals,
                EarningTimingInterval = existingOrderToCashCycle.EarningTimingInterval,
                EarningTimingType = existingOrderToCashCycle.EarningTimingType,
                IsEarnedImmediately = existingOrderToCashCycle.IsEarnedImmediately,
                QuantityChargeTimingType = existingOrderToCashCycle.QuantityChargeTimingType,
                QuantityProrateGranularity = existingOrderToCashCycle.QuantityProrateGranularity,
                QuantityProrateNegativeQuantity = existingOrderToCashCycle.QuantityProrateNegativeQuantity,
                QuantityProratePositiveQuantity = existingOrderToCashCycle.QuantityProratePositiveQuantity,
                QuantityReverseChargeNegativeQuantity = existingOrderToCashCycle.QuantityReverseChargeNegativeQuantity,
                RecurChargeTimingType = existingOrderToCashCycle.RecurChargeTimingType,
                RecurProrateGranularity = existingOrderToCashCycle.RecurProrateGranularity,
                RecurProrateNegativeQuantity = existingOrderToCashCycle.RecurProrateNegativeQuantity,
                RecurProratePositiveQuantity = existingOrderToCashCycle.RecurProratePositiveQuantity,
                RecurReverseChargeNegativeQuantity = existingOrderToCashCycle.RecurReverseChargeNegativeQuantity
            };

            CloneQuantityRanges(orderToCashCycle, existingPlanProduct);

            return orderToCashCycle;
        }

        private void CloneQuantityRanges(OrderToCashCycle newOrderToCashCycle, PlanProduct existingPlanProduct)
        {
            var quantityRanges =
                existingPlanProduct.PlanOrderToCashCycles.First()
                    .QuantityRanges.Select(existingQuantityRange => new QuantityRange
                    {
                        OrderToCashCycle = newOrderToCashCycle,
                        Prices = existingQuantityRange.Prices.Select(existingPrice => new Price
                        {
                            Amount = null,
                            CurrencyId = existingPrice.CurrencyId
                        }).ToList(),
                        Min = existingQuantityRange.Min,
                        Max = existingQuantityRange.Max
                    }).ToList();

            newOrderToCashCycle.QuantityRanges = quantityRanges;
        }
    }
}
