namespace Model.PdfViewModels
{
    public class Discount
    {
        public string Description { get; set; }
        public string DiscountType { get; set; }
        public decimal ConfiguredDiscountAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
    }
}
