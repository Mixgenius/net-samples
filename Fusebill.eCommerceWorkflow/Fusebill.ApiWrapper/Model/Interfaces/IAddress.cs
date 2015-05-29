namespace Model.Interfaces
{
    public interface IAddress
    {
        string CompanyName { get; }
        string Line1 { get; }
        string Line2 { get; }
        long CountryId { get; }
        long? StateId { get; }
        string City { get; }
        string PostalZip { get; }
        AddressType AddressType { get; }
        string CountryName { get; }
        string StateName { get; }
    }
}