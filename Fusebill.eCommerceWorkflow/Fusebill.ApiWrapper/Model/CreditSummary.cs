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
    
    public partial class CreditSummary : Entity
    {
        public System.DateTime EffectiveTimestamp { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public long CustomerId { get; set; }
        public TransactionType TransactionType { get; set; }
        public long AccountId { get; set; }
        public string Reference { get; set; }
    }
}
