using System.Linq;

namespace Model
{
    public partial class BillingPeriodDefinition
    {

        public BillingPeriod OpenBillingPeriod
        {
            get { return BillingPeriods.SingleOrDefault(bp => bp.PeriodStatus == PeriodStatus.Open); }
        }
    }
}
