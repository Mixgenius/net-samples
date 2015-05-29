using System.Collections.Generic;

namespace Model.Internal
{
    public class DraftChargeGroup
    {
        public DraftChargeGroup()
        {
            DraftCharges = new List<DraftCharge>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Reference { get; set; }

        public List<DraftCharge> DraftCharges { get; set; }
    }
}
