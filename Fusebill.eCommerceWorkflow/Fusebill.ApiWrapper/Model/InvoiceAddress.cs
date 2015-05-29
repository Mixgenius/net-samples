//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class InvoiceAddress : Entity
    {
        public System.DateTime ModifiedTimestamp { get; set; }
        public System.DateTime CreatedTimestamp { get; set; }
        public long InvoiceId { get; set; }
        public string CompanyName { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public long CountryId { get; set; }
        public Nullable<long> StateId { get; set; }
        public string City { get; set; }
        public string PostalZip { get; set; }
        public AddressType AddressType { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
    
        public virtual Invoice Invoice { get; set; }
    }
}
