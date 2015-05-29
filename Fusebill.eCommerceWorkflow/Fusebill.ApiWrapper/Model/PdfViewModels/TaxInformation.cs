namespace Model.PdfViewModels
{
    public class TaxInformation
    {
        public decimal Amount { get; set; }
        public string TaxRuleName { get; set; }
        public string TaxRulePercentage { get; set; }
        public string TaxRuleRegistration { get; set; }
    }
}