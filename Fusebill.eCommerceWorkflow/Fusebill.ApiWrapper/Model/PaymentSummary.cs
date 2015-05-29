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
    
    public partial class PaymentSummary : Entity
    {
        public Nullable<long> TransactionId { get; set; }
        public System.DateTime EffectiveTimestamp { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public long CustomerId { get; set; }
        public string PaymentSource { get; set; }
        public long AccountId { get; set; }
        public string Reference { get; set; }
        public Nullable<long> OriginalPaymentId { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentType { get; set; }
        public Nullable<TransactionType> TransactionType { get; set; }
        public string AccountType { get; set; }
        public string CardNumber { get; set; }
    }
}
