using Model.Interfaces;

namespace Model
{
    public partial class SubscriptionProductDiscount : IDiscountConfiguration
    {
        public bool ToBeDeleted { get; set; }

        public bool IsApplicable()
        {
            if (!(RemainingUsagesUntilStart == 0 && (!RemainingUsage.HasValue || RemainingUsage.Value > 0))) return false;

            return true;
        }

        public decimal RemainingAmount { get; set; }
    }
}
