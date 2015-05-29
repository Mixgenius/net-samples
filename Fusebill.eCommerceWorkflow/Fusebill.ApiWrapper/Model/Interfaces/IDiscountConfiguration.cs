namespace Model.Interfaces
{
    public interface IDiscountConfiguration
    {
        DiscountType DiscountType { get; set; }
        decimal Amount { get; set; }
        decimal RemainingAmount { get; set; }
        long? CouponCodeId { get; set; }

        CouponCode CouponCode { get; set; }

        bool IsApplicable();
    }
}
