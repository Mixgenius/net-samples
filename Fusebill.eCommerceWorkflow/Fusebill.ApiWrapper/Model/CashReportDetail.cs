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
    
    public partial class CashReportDetail : Entity
    {
        public long AccountId { get; set; }
        public System.DateTime EffectiveTimestamp { get; set; }
        public Nullable<long> TransactionId { get; set; }
        public long FusebillId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public string PaymentReference { get; set; }
        public string Source { get; set; }
        public string Type { get; set; }
        public string PaymentMethod { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public long Currency { get; set; }
    }
}
