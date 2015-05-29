using System;
using System.Collections.Generic;

namespace Model.Internal
{
    public class BillingStatementSummary
    {
        public BillingStatementHeader Header { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public string Currency { get; set; }
        public List<TransactionSummary> Transactions { get; set; }

        public BillingStatement OriginalBillingStatement { get; set; }
    }

    public class TransactionSummary
    {
        public string TransactionType { get; set; }
        public decimal ArDebit { get; set; }
        public decimal ArCredit { get; set; }
        public int AssociatedOrder { get; set; }
    }
}
