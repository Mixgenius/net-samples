namespace Model.Internal
{
    public enum RefundMethodOptions
    {
        PaymentMethod = 0,
        Check = PaymentMethodType.Check,
        Cash = PaymentMethodType.Cash,
        DirectDeposit = PaymentMethodType.DirectDeposit
    }
}
