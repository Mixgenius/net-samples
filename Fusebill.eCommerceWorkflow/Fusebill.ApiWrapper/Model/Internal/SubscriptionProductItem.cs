using System.Collections.Generic;
using DataCommon.DataStructures;

namespace Model.Internal
{
    public class SubscriptionProductItem
    {
        public ProductItem ProductItem { get; set; }
        public InvoicePreview InvoicePreview { get; set; }
        public List<InvoicePreview> AdditionalInvoicesForPreview { get; set; }
        public DraftInvoiceDisplayOptions DraftInvoiceDisplayOptions { get; set; }
    }
}