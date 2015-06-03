using Fusebill.ApiWrapper.Dto.Interfaces;

namespace Fusebill.ApiWrapper.Dto.Put
{
    public class SubscriptionProductDiscount : BaseDto, ISubscriptionProductDiscount
    {
        public string DiscountType { get; set; }
        public decimal Amount { get; set; }
        public int RemainingUsagesUntilStart { get; set; }
        public int? RemainingUsage { get; set; }
    }
}
