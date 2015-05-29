using System.Collections.Generic;
using System.Linq;
using Model.Internal;

namespace Model
{
    public class InvoicePreview
    {
        public decimal Total
        {
            get { return Subtotal + DraftTaxes.Sum(t => t.Amount); }
        }

        public decimal Subtotal
        {
            get { return DraftCharges.Sum(c => c.Amount) - DraftCharges.Sum(dc => dc.DraftDiscounts.Sum(dis => dis.Amount)); }
        }

        public decimal TotalTaxes
        {
            get { return DraftTaxes.Sum(t => t.Amount); }
        }

        public string PoNumber { get; set; }

        public DraftInvoiceStatus Status { get; set; }

        public ICollection<DraftCharge> DraftCharges { get; set; }
        public ICollection<DraftTax> DraftTaxes { get; set; }

        public List<DraftChargeGroup> DraftChargeGroups { get; set; }

        public string Notes { get; set; }

        public string ShippingInstructions { get; set; }

        public Address BillingAddress { get; set; }

        public Account Account { get; set; }
    }
}
