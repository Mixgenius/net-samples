using System;

namespace Model.PdfViewModels
{
    public class PaymentSchedule
    {
        public DateTime DueDateTimestamp { get; set; }

        public string Status { get; set; }

        public decimal Amount { get; set; }

        public decimal OutstandingBalance { get; set; }

        public int DaysDueAfterTerm { get; set; }
    }
}