using System;
using System.Collections.Generic;
using System.Linq;
using DataCommon.DataStructures;
using DataCommon.Models;
using Model.Interfaces;
using Newtonsoft.Json;

namespace Model
{
    public partial class Subscription : IIntegrationSyncable
    {
        public string Name
        {
            get
            {
                if (PlanFrequency.PlanRevision == null)
                    throw new FusebillException("PlanRevision is null.  Please make sure to have all your EntityFramework includes.");

                if (PlanFrequency.PlanRevision.Plan == null)
                    throw new FusebillException("Plan is null.  Please make sure to have all your EntityFramework includes.");

                string name = PlanFrequency.PlanRevision.Plan.Name;

                if (SubscriptionOverride != null && !string.IsNullOrEmpty(SubscriptionOverride.Name))
                {
                    name = SubscriptionOverride.Name;
                }

                return name;
            }
        }

        public string Description
        {
            get
            {
                if (PlanFrequency.PlanRevision == null)
                    throw new FusebillException("PlanRevision is null.  Please make sure to have all your EntityFramework includes.");

                if (PlanFrequency.PlanRevision.Plan == null)
                    throw new FusebillException("Plan is null.  Please make sure to have all your EntityFramework includes.");

                string description = PlanFrequency.PlanRevision.Plan.Description;

                if (SubscriptionOverride != null && !string.IsNullOrEmpty(SubscriptionOverride.Description))
                {
                    description = SubscriptionOverride.Description;
                }

                return description;
            }
        }

        [JsonIgnore]
        public BillingPeriod OpenBillingPeriod
        {
            get
            {
                if (null == BillingPeriodDefinition)
                    return null;

                return BillingPeriodDefinition.OpenBillingPeriod;
            }
        }

        [JsonIgnore]
        public BillingPeriod PreviousBillingPeriod
        {
            get
            {
                if (null == BillingPeriodDefinition)
                    return null;

                return
                    BillingPeriodDefinition.BillingPeriods.OrderByDescending(bp => bp.EndDate)
                        .FirstOrDefault(bp => bp.PeriodStatus == PeriodStatus.Closed);
            }
        }
        
        public DateTime? OpenSubscriptionPeriodEndDate
        {
            get
            {
                if (BillingPeriodDefinition == null || BillingPeriodDefinition.BillingPeriods.Count < 1 || ActivationTimestamp == null || RemainingInterval == 0)
                    return null;

                return OpenBillingPeriod.EndDate;
            }
        }

        public InvoicePreview InvoicePreview { get; set; }
        public List<InvoicePreview> AdditionalInvoicesForPreview { get; set; }
        public DraftInvoiceDisplayOptions DraftInvoiceDisplayOptions { get; set; }

        public bool IsEligibleSubscription()
        {
            return Status == SubscriptionStatus.Active || Status == SubscriptionStatus.Provisioning;
        }

        public SubscriptionProduct GetChargeSubscriptionProduct()
        {
            return SubscriptionProducts.SingleOrDefault(
                    s => s.PlanProduct.Product.ProductType == ProductType.PlanCharge);
        }

        public SubscriptionProduct GetSetupFeeSubscriptionProduct()
        {
            return SubscriptionProducts.SingleOrDefault(
                    s => s.PlanProduct.Product.ProductType == ProductType.PlanSetupFee);
        }
    }
}
