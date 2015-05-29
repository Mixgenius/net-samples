using Model.Interfaces;

namespace Model
{
    public partial class InvoiceAddress : IAddress
    {
        public string CountryName { get { return Country; } }
        public string StateName { get { return State; } }
    }
}
