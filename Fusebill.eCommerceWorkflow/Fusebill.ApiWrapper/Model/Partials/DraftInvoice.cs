using System.Collections.Generic;
using Model.Internal;

namespace Model
{
    public partial class DraftInvoice
    {
        public List<DraftChargeGroup> DraftChargeGroups { get; set; }
    }
}
