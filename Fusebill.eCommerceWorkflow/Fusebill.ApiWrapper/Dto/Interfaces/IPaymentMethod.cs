using System;

namespace Fusebill.ApiWrapper.Dto.Interfaces
{
    public interface IPaymentMethod
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        string City { get; set; }
        Nullable<long> StateId { get; set; }
        Nullable<long> CountryId { get; set; }
        string PostalZip { get; set; }
        string Source { get; set; }
    }
}
