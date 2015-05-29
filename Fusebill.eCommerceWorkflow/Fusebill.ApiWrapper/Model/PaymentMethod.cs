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
    
    public partial class PaymentMethod : Entity
    {
        public PaymentMethod()
        {
            this.PaymentActivityJournals = new HashSet<PaymentActivityJournal>();
        }
    
        public long CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public Nullable<long> StateId { get; set; }
        public Nullable<long> CountryId { get; set; }
        public string PostalZip { get; set; }
        public string Token { get; set; }
        public PaymentMethodStatus PaymentMethodStatus { get; set; }
        public string AccountType { get; set; }
        public Nullable<PaymentMethodType> PaymentMethodType { get; set; }
        public bool IsDefault { get; set; }
        public string ExternalCustomerId { get; set; }
        public string ExternalCardId { get; set; }
        public bool StoredInFusebillVault { get; set; }
    
        public virtual ICollection<PaymentActivityJournal> PaymentActivityJournals { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
