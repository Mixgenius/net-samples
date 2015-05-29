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
    
    public partial class InvoiceSummary : Entity
    {
        public long CustomerId { get; set; }
        public string Reference { get; set; }
        public long CurrencyId { get; set; }
        public long AccountId { get; set; }
        public string InvoiceNumber { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
        public decimal SumOfCharges { get; set; }
        public System.DateTime DueDate { get; set; }
        public System.DateTime PostedTimestamp { get; set; }
        public System.DateTime EffectiveTimestamp { get; set; }
        public decimal OutstandingBalance { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal TotalCreditNotes { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal Writeoffs { get; set; }
        public long DraftInvoiceId { get; set; }
    }
}
