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
    
    public partial class BillingPeriod : Entity
    {
        public BillingPeriod()
        {
            this.DraftInvoices = new HashSet<DraftInvoice>();
            this.Invoices = new HashSet<Invoice>();
            this.DraftSubscriptionProductCharges = new HashSet<DraftSubscriptionProductCharge>();
            this.SubscriptionProductCharges = new HashSet<SubscriptionProductCharge>();
        }
    
        public System.DateTime CreatedTimestamp { get; set; }
        public System.DateTime ModifiedTimestamp { get; set; }
        public long CustomerId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public PeriodStatus PeriodStatus { get; set; }
        public long BillingPeriodDefinitionId { get; set; }
    
        public virtual ICollection<DraftInvoice> DraftInvoices { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual BillingPeriodDefinition BillingPeriodDefinition { get; set; }
        public virtual ICollection<DraftSubscriptionProductCharge> DraftSubscriptionProductCharges { get; set; }
        public virtual ICollection<SubscriptionProductCharge> SubscriptionProductCharges { get; set; }
    }
}
