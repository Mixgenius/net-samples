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
    
    public partial class AvalaraConfiguration : Entity
    {
        public bool Enabled { get; set; }
        public string AccountNumber { get; set; }
        public string LicenseKey { get; set; }
        public string OrganizationCode { get; set; }
        public bool DevMode { get; set; }
        public bool UseCustomerBillingAddress { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public Nullable<long> CountryId { get; set; }
        public Nullable<long> StateId { get; set; }
        public string City { get; set; }
        public string PostalZip { get; set; }
        public string Salt { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
    }
}
