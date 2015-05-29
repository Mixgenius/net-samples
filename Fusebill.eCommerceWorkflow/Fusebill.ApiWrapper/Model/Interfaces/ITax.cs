namespace Model.Interfaces
{
    public interface ITax
    {
        decimal Amount { get; }
        long TaxRuleId { get; }
        string Name { get; }
        string RegistrationCode { get; }
        decimal Percentage { get; }
    }
}
