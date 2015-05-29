namespace Model.Internal
{
    public enum PaymentMethodOptions
    {
        UseDefaultPaymentMethod,
        UseExistingPaymentMethod,
        UseExistingPaymentMethodAndMakeDefault,
        UseProvidedPaymentMethodOnce,
        UseProvidedPaymentMethodAndMakeDefault,
        UseProvidedPaymentMethodAndSave,
        CreateAndApplyCredit
    }
}
