using System;
using System.Linq;

namespace Model
{
    public partial class PaymentSchedule
    {
        public decimal OutstandingBalance
        {
            get
            {
                return MostRecentPaymentScheduleJournal.OutstandingBalance;
            }
        }

        public DateTime DueDate
        {
            get
            {
                return MostRecentPaymentScheduleJournal.DueDate;
            }
        }

        public InvoiceStatus Status
        {
            get
            {
                return MostRecentPaymentScheduleJournal.Status;
            }
        }

        private PaymentScheduleJournal MostRecentPaymentScheduleJournal
        {
            get
            {
                return PaymentScheduleJournals.FirstOrDefault(psj => psj.IsActive);
            }
        }
    }
}
