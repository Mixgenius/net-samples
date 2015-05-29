using Model.Interfaces;

namespace Model
{
    public partial class PurchaseDiscount : IDiscountConfiguration
    {
        public decimal RemainingAmount { get; set; }

        public bool IsApplicable()
        {
            return true;
        }
    }
}
