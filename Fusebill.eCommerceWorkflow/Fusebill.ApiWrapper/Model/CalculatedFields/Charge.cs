using System;
using System.Linq;
using DataCommon.Models;

namespace Model
{
    public partial class Charge
    {
        public DateTime LastEarningTimestamp
        {
            get
            {
                if (Earnings == null || Earnings.Count == 0)
                {
                    return EarningStartDate;
                }
                
                return Earnings.OrderByDescending(e => e.EffectiveTimestamp).FirstOrDefault().EffectiveTimestamp;
            }
        }

        public decimal UnearnedAmount
        {
            get
            {
                if (null == Earnings)
                    throw new FusebillException("Please ensure earnings are included with the charge");

                return Math.Max(0, RemainingReverseAmount - Earnings.Sum(e => e.Amount) + ReverseCharges.Sum(rc => rc.ReverseEarnings.Sum(re => re.Amount)));
            }
        }

        public decimal TaxableAmount()
        {
            return Amount - Discounts.Sum(d => d.Amount);
        }
    }
}
