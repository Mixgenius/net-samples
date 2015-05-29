using Model.Interfaces;

namespace Model
{
    public partial class Address : IAddress
    {
        public string CountryIso2 { get { return Country != null ? Country.ISO : string.Empty; } }
        public string StateIso3 { get { return State != null ? State.SubdivisionISOCode : string.Empty; } }
        public string CountryName { get { return Country != null ? Country.Name : string.Empty; } }
        public string StateName { get { return State != null ? State.Name : string.Empty; } }
    }
}
