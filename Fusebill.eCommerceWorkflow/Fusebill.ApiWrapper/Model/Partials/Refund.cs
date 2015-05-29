using System.Collections.Generic;
using Model.Internal;

namespace Model
{
    public partial class Refund
    {
        public List<RefundAllocation> RefundAllocations { get; set; }

        public RefundMethodOptions Method { get; set; }
        
        public void AddRefundNote(RefundNote refundNote)
        {
            RefundNotes.Add(refundNote);
        }
    }
}
