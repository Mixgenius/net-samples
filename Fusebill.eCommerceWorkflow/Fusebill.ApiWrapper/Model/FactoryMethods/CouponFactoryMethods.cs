using System.Collections.Generic;
using System.Linq;

namespace Model.FactoryMethods
{
    public class CouponFactoryMethods
    {
        public static Dictionary<long, string> ConvertSubscriptionCouponCodes(ICollection<SubscriptionCouponCode> subscriptionCouponCodes)
        {
            return
                subscriptionCouponCodes.Where(scc => scc.Status == SubscriptionCouponCodeStatus.Active)
                    .ToDictionary(subscriptionCouponCode => subscriptionCouponCode.CouponCodeId,
                        subscriptionCouponCode => subscriptionCouponCode.CouponCode.Code);
        }

        public static Dictionary<long, string> ConvertPurchaseCouponCodes(ICollection<PurchaseCouponCode> purchaseCouponCodes)
        {
            return
                purchaseCouponCodes
                    .ToDictionary(cc => cc.CouponCodeId,
                        cc => cc.CouponCode.Code);
        }
    }
}
