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
    
    public partial class PaymentScheduleJournal : Entity
    {
        public long PaymentScheduleId { get; set; }
        public System.DateTime DueDate { get; set; }
        public InvoiceStatus Status { get; set; }
        public System.DateTime CreatedTimestamp { get; set; }
        public decimal OutstandingBalance { get; set; }
        public bool IsActive { get; set; }
    
        public virtual PaymentSchedule PaymentSchedule { get; set; }
    }
}
