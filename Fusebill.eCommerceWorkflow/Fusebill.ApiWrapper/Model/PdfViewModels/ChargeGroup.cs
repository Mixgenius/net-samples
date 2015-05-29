using System.Collections.Generic;

namespace Model.PdfViewModels
{
    public class ChargeGroup
    {
        public ChargeGroup()
        {
            Charges = new List<Charge>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public List<Charge> Charges { get; set; }
    }
}
