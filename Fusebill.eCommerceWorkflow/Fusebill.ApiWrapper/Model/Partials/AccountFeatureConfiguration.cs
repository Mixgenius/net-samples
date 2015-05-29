namespace Model
{
    public partial class AccountFeatureConfiguration
    {
        public string GetOrganizationCode()
        {
            if (TaxOption == TaxOption.AvalaraDirectTaxation && Account.AvalaraConfiguration != null)
                return Account.AvalaraConfiguration.OrganizationCode;

            return AvalaraOrganizationCode;
        }

        public bool AvalaraEnabled()
        {
            if (TaxOption == TaxOption.AdvancedTaxation)
                return true;

            return AvalaraDirectEnabled();
        }

        public bool UseBillingAddressForTaxes()
        {
            if (TaxOption == TaxOption.AvalaraDirectTaxation)
                return Account.AvalaraConfiguration != null && Account.AvalaraConfiguration.UseCustomerBillingAddress;

            return true;
        }

        public bool AvalaraDirectEnabled()
        {
            if (TaxOption == TaxOption.AvalaraDirectTaxation)
                return Account.AvalaraConfiguration != null && Account.AvalaraConfiguration.Enabled;

            return false;
        }
    }
}
