using System;
using System.Collections.Generic;
using System.Linq;
using DataCommon.DataStructures;
using DataCommon.Models;
using Model.Interfaces;
using Newtonsoft.Json;

namespace Model
{
    public partial class SubscriptionProduct : IIntegrationSyncable
    {
        [JsonIgnore]
        public PlanOrderToCashCycle PlanOrderToCashCycle
        {
            get { return PlanProduct.PlanOrderToCashCycles.First(o => o.PlanFrequencyId == Subscription.PlanFrequencyId); }
        }

        [JsonIgnore]
        public decimal TotalQuantityChargedInCurrentOpenPeriod
        {
            get
            {
                var openSubscriptionPeriod = Subscription.OpenBillingPeriod;

                if (openSubscriptionPeriod == null) throw new FusebillException("Unable to find open subscription period.");

                return SubscriptionProductCharges.Where(c => c.BillingPeriodId == openSubscriptionPeriod.Id).Sum(item => item.Charge.Quantity);
            }
        }

        [JsonIgnore]
        public List<DraftSubscriptionProductCharge> OpenSubscriptionPeriodProductDraftCharges
        {
            get
            {
                if (Subscription == null) return new List<DraftSubscriptionProductCharge>();

                if (Subscription.BillingPeriodDefinition == null || Subscription.BillingPeriodDefinition.BillingPeriods.Count == 0)
                    return new List<DraftSubscriptionProductCharge>();

                var openSubscriptionPeriod = Subscription.OpenBillingPeriod;

                return DraftSubscriptionProductCharges.Where(c => c.BillingPeriod == openSubscriptionPeriod).ToList();
            }
        }

        [JsonIgnore]
        public List<Charge> OpenSubscriptionPeriodProductCharges
        {
            get
            {
                if (Subscription == null) return new List<Charge>();

                if (Subscription.BillingPeriodDefinition == null || Subscription.BillingPeriodDefinition.BillingPeriods.Count == 0)
                    return new List<Charge>();

                var openSubscriptionPeriod = Subscription.OpenBillingPeriod;

                return SubscriptionProductCharges.Where(c => c.BillingPeriod == openSubscriptionPeriod).Select(dc => dc.Charge).ToList();
            }
        }

        [JsonIgnore]
        public bool HasNotBeenChargeInOpenSubscriptionPeriodAndNotBillInTheRear
        {
            get
            {
                return OpenSubscriptionPeriodProductDraftCharges.Count == 0 &&
                       OpenSubscriptionPeriodProductDraftCharges.Count == 0 &&
                       PlanOrderToCashCycle.RecurChargeTimingType != ChargeTimingType.EndOfPeriod;
            }
        }

        public bool ShouldResetAtStartOfPeriod()
        {
            return PlanProduct.ResetType == ProductResetType.StartOfPeriod && Quantity > 0;
        }

        public bool ShouldResetAtEndOfPeriod()
        {
            return PlanProduct.ResetType == ProductResetType.EndOfPeriod && Quantity > 0;
        }

        public bool ShouldChargeRecurringProductAtStartOfPeriod()
        {
            return PlanProduct.IsRecurring && Included &&
                   (PlanOrderToCashCycle.RecurChargeTimingType == ChargeTimingType.StartOfPeriod ||
                    PlanOrderToCashCycle.RecurChargeTimingType == ChargeTimingType.Immediate)
                   && OpenSubscriptionPeriodProductCharges.Count == 0
                   && OpenSubscriptionPeriodProductDraftCharges.Count == 0;
        }

        public bool ShouldChargeAtEndOfPeriod()
        {
            return PlanOrderToCashCycle.RecurChargeTimingType == ChargeTimingType.EndOfPeriod ||
                   PlanOrderToCashCycle.QuantityChargeTimingType == ChargeTimingType.EndOfPeriod;
        }

        public bool ShouldTrackEndOfPeriodQuantity()
        {
            return PlanOrderToCashCycle.RecurChargeTimingType == ChargeTimingType.EndOfPeriod && Included && Quantity > 0;
        }

        public bool ShouldChargeProductAtActivation(bool isReactivating = false)
        {
            return Included &&
                   (PlanOrderToCashCycle.RecurChargeTimingType == ChargeTimingType.StartOfPeriod ||
                    PlanOrderToCashCycle.RecurChargeTimingType == ChargeTimingType.Immediate) &&
                   (isReactivating || (DraftSubscriptionProductCharges.Count(dc => dc.DraftCharge.Status == DraftChargeStatus.Active) == 0 && SubscriptionProductCharges.Count == 0));
        }

        public bool ProductIsNotDelayedStart(ITimeOfTransaction timeOfTransaction)
        {
            return !StartDate.HasValue || StartDate.Value <= timeOfTransaction.Timestamp;
        }

        public DateTime DetermineServiceEndDateForEndOfPeriodQuantityChange(SubscriptionProductActivityJournal subscriptionProductActivityJournal)
        {
            if (PlanProduct.IsRecurring)
                return Subscription.PreviousBillingPeriod.EndDate;

            return subscriptionProductActivityJournal.CreatedTimestamp;
        }

        public DateTime DetermineServiceEndDateForCharge(DateTime startServiceDate, bool chargeOnPreviousPeriod = false)
        {
            if (PlanProduct.IsRecurring)
                return chargeOnPreviousPeriod
                    ? Subscription.PreviousBillingPeriod.EndDate
                    : Subscription.OpenBillingPeriod.EndDate;

            return startServiceDate;
        }

        public DateTime DetermineServiceEndDateForActivation()
        {
            if (PlanProduct.IsRecurring)
                return Subscription.OpenBillingPeriod.EndDate;

            return Subscription.ActivationTimestamp.Value;
        }

        public bool ShouldSumSpajAsOneCharge()
        {
            return PlanOrderToCashCycle.RecurChargeTimingType == ChargeTimingType.EndOfPeriod &&
                   PlanOrderToCashCycle.QuantityChargeTimingType == ChargeTimingType.EndOfPeriod &&
                   !PlanOrderToCashCycle.QuantityProratePositiveQuantity &&
                   PlanProduct.ResetType == ProductResetType.EndOfPeriod && Included;
        }

        public InvoicePreview InvoicePreview { get; set; }
        public DraftInvoiceDisplayOptions DraftInvoiceDisplayOptions { get; set; }

        public bool SubscriptionProductEligibleToCharge(ITimeOfTransaction timeOfTransaction)
        {
            if ((PlanProduct.Product.ProductType == ProductType.OneTimeCharge ||
                 PlanProduct.Product.ProductType == ProductType.PhysicalGood ||
                 PlanProduct.Product.ProductType == ProductType.PlanSetupFee) &&
                Subscription.Status != SubscriptionStatus.Active &&
                ChargeAtSubscriptionActivation &&
                Subscription.ScheduledActivationTimestamp.HasValue &&
                Subscription.ScheduledActivationTimestamp.Value > timeOfTransaction.Timestamp)
                return false;

            return true;
        }

        public bool IsEligibleSubscriptionProduct(DateTime? accountUtcTime)
        {
            return Included &&
                   (StartDate.HasValue && StartDate.Value < accountUtcTime && !IsCharged)
                   && Status == SubscriptionProductStatus.Active;
        }
    }
}
