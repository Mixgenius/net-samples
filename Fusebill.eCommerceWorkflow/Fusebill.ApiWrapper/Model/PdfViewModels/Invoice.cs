using System;
using System.Collections.Generic;

namespace Model.PdfViewModels
{
    public class Invoice
    {
        public Invoice()
        {
            ChargeGroups = new List<ChargeGroup>();
        }

        public Int32 InvoiceNumber { get; set; }
        public string Signature { get; set; }
        public string PoNumber { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal Payments { get; set; }
        public decimal Outstanding { get; set; }
        public string PostedDate { get; set; }
        public string DueDate { get; set; }
        public string Terms { get; set; }
        public string Status { get; set; }
        public ICollection<TaxInformation> Taxes { get; set; } 
        public BillingInformation BillingInformation { get; set; }
        public ShippingInformation ShippingInformation { get; set; }
        public ICollection<ChargeGroup> ChargeGroups { get; set; }
        public decimal SumOfCreditNotes { get; set; }
        public decimal TotalWriteoffs { get; set; }
        public ICollection<PaymentSchedule> PaymentSchedules { get; set; }
        public Currency Currency { get; set; }
        public Account Account { get; set;}
        public ContactInformation ContactInformation { get; set; }
        public string Notes { get; set; }
    }
}
