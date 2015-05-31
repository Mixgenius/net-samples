//using Common.Dto.Interfaces;
using Fusebill.ApiWrapper.Dto.Interfaces;

namespace Fusebill.ApiWrapper.Dto.Post
{
    public class SubscriptionProductDiscount : BaseDto, ISubscriptionProductDiscount
    {
        public string DiscountType { get; set; }
        public decimal Amount { get; set; }
        public int RemainingUsagesUntilStart { get; set; }
        public int? RemainingUsage { get; set; }
    }
}
