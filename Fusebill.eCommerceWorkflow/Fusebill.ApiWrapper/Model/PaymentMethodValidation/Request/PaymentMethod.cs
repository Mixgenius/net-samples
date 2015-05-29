namespace Model.PaymentMethodValidation.Request
{
    public class PaymentMethod
    {
        public CreditCard CreditCard { get; set; }
        public AchCard AchAccount { get; set; }
    }
}
