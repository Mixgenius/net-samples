using System.Collections.Generic;

namespace Model.Internal
{
    public class BulkPurchase
    {
        public List<Purchase> Purchases { get; set; }

        public InvoiceCollectOptions InvoiceCollectOptions { get; set; }
    }
}
