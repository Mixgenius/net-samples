using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class Invoice : BaseDto
    {
        private readonly bool _serializeChargeGroups;

        public Invoice(bool serializeChargeGroups = false)
        {
            _serializeChargeGroups = serializeChargeGroups;
            Taxes = new List<Tax>();
            Charges = new List<InvoiceCharge>();
            PaymentSchedules = new List<PaymentSchedule>();
        }

        [JsonProperty(PropertyName = "invoiceNumber")]
        [DisplayName("Invoice Number")]
        public long InvoiceNumber { get; set; }

        [JsonProperty(PropertyName = "invoiceSignature")]
        [DisplayName("Invoice Signature")]
        public String InvoiceSignature { get; set; }

        [JsonProperty(PropertyName = "poNumber")]
        [DisplayName("PO Number")]
        public String PoNumber { get; set; }

        [JsonProperty(PropertyName = "effectiveTimestamp")]
        [DisplayName("Effective Date")]
        public DateTime EffectiveDateTimestamp { get; set; }

        [JsonProperty(PropertyName = "postedTimestamp")]
        [DisplayName("Posted Date")]
        public DateTime PostedDateTimestamp { get; set; }

        [JsonProperty(PropertyName = "charges")]
        [DisplayName("Charges")]
        public List<InvoiceCharge> Charges { get; set; }

       // [JsonProperty(PropertyName = "chargeGroups")]
       // [DisplayName("Charge Groups")]
       //public List<ChargeGroup> ChargeGroups { get; set; }

        [JsonProperty(PropertyName = "subtotal")]
        [DisplayName("Subtotal")]
        public decimal Subtotal { get; set; }

        [JsonProperty(PropertyName = "invoiceAmount")]
        [DisplayName("Invoice Amount")]
        public decimal InvoiceAmount { get; set; }

        [JsonProperty(PropertyName = "totalPayments")]
        [DisplayName("Total Payments")]
        public decimal TotalPayments { get; set; }

        [JsonProperty(PropertyName = "outstandingBalance")]
        [DisplayName("Outstanding Balance")]
        public decimal OutstandingBalance { get; set; }

        [JsonProperty(PropertyName = "terms")]
        [DisplayName("Terms")]
        public string Terms { get; set; }

        [JsonProperty(PropertyName = "customerId")]
        [DisplayName("Customer Id")]
        public long CustomerId { get; set; }

        [JsonProperty(PropertyName = "invoiceCustomer")]
        [DisplayName("Invoice Customer")]
        public Customer InvoiceCustomer { get; set; }

        //[JsonProperty(PropertyName = "invoiceCustomerAddressPreference")]
        //[DisplayName("Invoice Customer Address Preference")]
        //public CustomerAddressPreference InvoiceCustomerAddressPreference { get; set; }

        [JsonProperty(PropertyName = "sumOfCreditNotes")]
        [DisplayName("Sum of credit notes")]
        public decimal SumOfCreditNotes { get; set; }

        [JsonProperty(PropertyName = "totalWriteoffs")]
        [DisplayName("Total writeoffs")]
        public decimal TotalWriteoffs { get; set; }

        [JsonProperty(PropertyName = "taxes")]
        public List<Tax> Taxes { get; set; }

        [JsonProperty(PropertyName = "paymentSchedules")]
        public List<PaymentSchedule> PaymentSchedules { get; set; }

        [JsonProperty(PropertyName = "notes")]
        [DisplayName("Notes")]
        public string Notes { get; set; }

        [JsonProperty(PropertyName = "customerReferenceValue")]
        [DisplayName("Customer Reference Value")]
        public string CustomerReferenceValue { get; set; }

        public bool ShouldSerializeCharges()
        {
            return !_serializeChargeGroups;
        }

        //public bool ShouldSerializeChargeGroups()
        //{
        //    return _serializeChargeGroups || ChargeGroups != null;
        //}
    }
}
