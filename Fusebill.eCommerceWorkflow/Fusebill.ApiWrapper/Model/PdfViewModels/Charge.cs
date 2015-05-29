using System;
using System.Collections.Generic;

namespace Model.PdfViewModels
{
    public class Charge
    {
        public Charge()
        {
            Discounts = new List<Discount>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartServiceDate { get; set; }
        public DateTime EndServiceDate { get; set; }
        public decimal? RangeQuantity { get; set; }
        public dynamic Quantity { get; set; }
        public decimal? ProratedUnitPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        public ICollection<Discount> Discounts { get; set; }
        public ICollection<ProductItem> ProductItems { get; set; }
    }
}