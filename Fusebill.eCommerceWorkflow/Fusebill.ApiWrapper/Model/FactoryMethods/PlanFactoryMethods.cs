using System;

namespace Model.FactoryMethods
{
    public static class PlanFactoryMethods
    {
        public static Price CreatePrice(QuantityRange quantityRange, decimal? amount, long currencyId, long id)
        {
            var price = new Price
            {
                Amount = amount,
                CurrencyId = currencyId,
                Id = id,
                QuantityRangeId = quantityRange.Id
            };

            quantityRange.Prices.Add(price);

            return price;
        }

        public static QuantityRange CreateQuantityRange(OrderToCashCycle orderToCashCycle, DateTime createdTimestamp, long id, decimal? max, decimal min, DateTime modifiedTimestamp)
        {
            var quantityRange = new QuantityRange
            {
                CreatedTimestamp = createdTimestamp,
                Id = id,
                Max = max,
                Min = min,
                ModifiedTimestamp = modifiedTimestamp,
                OrderToCashCycleId = orderToCashCycle.Id
            };

            orderToCashCycle.QuantityRanges.Add(quantityRange);

            return quantityRange;
        }

        public static void CreateRevenueRecognitionModel(OrderToCashCycle orderToCashCycle, DateTime createdTimestamp, Interval? earningInterval, int? earningNumberOfInterval, long id, bool isEarnedImmediately, DateTime modifiedTimestamp)
        {
            orderToCashCycle.EarningInterval = earningInterval;
            orderToCashCycle.EarningNumberOfInterval = earningNumberOfInterval;
            orderToCashCycle.IsEarnedImmediately = isEarnedImmediately;
        }

        public static PlanOrderToCashCycle CreateOrderToCashCycle(PlanProduct planProduct, PlanFrequency planFrequency, DateTime createdTimestamp, long id, DateTime modifiedTimestamp)
        {
            var orderToCashCycle = new PlanOrderToCashCycle
            {
                CreatedTimestamp = createdTimestamp,
                Id = id,
                ModifiedTimestamp = modifiedTimestamp,
                PlanFrequencyId = planFrequency.Id,
                PlanProductId = planProduct.Id
            };

            planProduct.PlanOrderToCashCycles.Add(orderToCashCycle);
            planFrequency.PlanOrderToCashCycles.Add(orderToCashCycle);

            return orderToCashCycle;
        }

        public static Product CreateProduct(PlanProduct planProduct, long accountId, string code, DateTime createdTimestamp, string description, long id, DateTime modifiedTimestamp, string name, ProductType productType, ProductStatus productStatus)
        {
            var product = new Product
            {
                AccountId = accountId,
                Code = code,
                CreatedTimestamp = createdTimestamp,
                Description = description,
                Id = id,
                ModifiedTimestamp = modifiedTimestamp,
                Name = name,
                ProductType = productType,
                Status = productStatus
            };

            planProduct.Product = product;

            return product;
        }

        public static PlanProduct CreatePlanProduct(PlanRevision planRevision, DateTime createdTimestamp, long id, bool isFixed, bool isIncludedByDefault, bool isOptional, bool isRecurring, bool isTrackingItems, decimal? maxQuantity, DateTime modifiedTimestamp, long productId, decimal quantity)
        {
            var planProduct = new PlanProduct
            {
                CreatedTimestamp = createdTimestamp,
                Id = id,
                IsFixed = isFixed,
                IsIncludedByDefault = isIncludedByDefault,
                IsOptional = isOptional,
                IsRecurring = isRecurring,
                IsTrackingItems = isTrackingItems,
                MaxQuantity = maxQuantity,
                ModifiedTimestamp = modifiedTimestamp,
                PlanRevisionId = planRevision.Id,
                ProductId = productId,
                Quantity = quantity,
                PlanProductKey = new PlanProductKey(),
                Status = PlanProductStatus.Active
            };

            planRevision.PlanProducts.Add(planProduct);

            return planProduct;
        }

        public static PlanFrequency CreatePlanFrequency(PlanRevision planRevision, long id, Interval interval, int numberOfIntervals, PlanFrequencyStatus status)
        {
            var planFrequency = new PlanFrequency
            {
                Id = id,
                Interval = interval,
                NumberOfIntervals = numberOfIntervals,
                PlanRevisionId = planRevision.Id,
                Status = status,
            };

            planRevision.PlanFrequencies.Add(planFrequency);

            return planFrequency;
        }

        public static PlanRevision CreatePlanRevision(Plan plan, DateTime createdTimestamp, long id, bool isActive)
        {
            var planRevision = new PlanRevision
            {
                CreatedTimestamp = createdTimestamp,
                Id = id,
                IsActive = isActive,
                PlanId = plan.Id
            };

            plan.PlanRevisions.Add(planRevision);

            return planRevision;
        }

        public static Plan CreatePlan(long id, long accountId, DateTime modifiedTimestamp, DateTime createdTimestamp, string code, string name, string description, PlanStatus status, string longDescription, string reference)
        {
            return new Plan
            {
                Id = id,
                AccountId = accountId,
                ModifiedTimestamp = modifiedTimestamp,
                CreatedTimestamp = createdTimestamp,
                Code = code,
                Name = name,
                Reference = reference,
                Description = description,
                Status = status,
                LongDescription = longDescription
            };
        }
    }
}
