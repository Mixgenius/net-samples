using Model.Interfaces;

namespace Model
{
    public partial class Tax : ITax
    {
        public string Name
        {
            get { return TaxRule.Name; }
        }

        public string RegistrationCode
        {
            get { return TaxRule.RegistrationCode; }
        }

        public decimal Percentage
        {
            get { return TaxRule.Percentage; }
        }
    }
}
