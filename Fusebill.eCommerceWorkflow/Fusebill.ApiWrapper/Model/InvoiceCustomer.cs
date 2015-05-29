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
    
    public partial class InvoiceCustomer : Entity
    {
        public long InvoiceId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string PrimaryEmail { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondaryEmail { get; set; }
        public string SecondaryPhone { get; set; }
        public Nullable<int> TitleId { get; set; }
        public string Reference { get; set; }
        public System.DateTime CreatedTimestamp { get; set; }
        public System.DateTime ModifiedTimestamp { get; set; }
        public System.DateTime EffectiveTimestamp { get; set; }
        public string ContactName { get; set; }
        public string ShippingInstructions { get; set; }
        public string Title { get; set; }
        public long CurrencyId { get; set; }
        public string CompanyName { get; set; }
    
        public virtual Currency Currency { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
