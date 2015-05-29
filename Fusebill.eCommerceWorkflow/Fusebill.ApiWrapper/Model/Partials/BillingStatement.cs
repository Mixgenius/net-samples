using System.Collections.Generic;

namespace Model
{
    public partial class BillingStatement
    {
        public BillingStatementHeader Header { get; set; }
        public string Currency { get; set; }
        public List<CustomerTransactionSummary> Transactions { get; set; }

        public bool IsSummarized { get; private set; }

        public void SetIsSummarized(CustomerBillingStatementSetting customerBillingStatementSetting, Account account)
        {
            bool isSummarized = (customerBillingStatementSetting.Option.HasValue &&
                               customerBillingStatementSetting.Option.Value == BillingStatementOption.Summarized)
                              || (!customerBillingStatementSetting.Option.HasValue
                                  && account.AccountBillingStatementPreference.Option == BillingStatementOption.Summarized);

            IsSummarized = isSummarized;
        }
    }

    public class BillingStatementHeader
    {
        public string Signature { get; set; }
        public BillingStatementCustomer Customer { get; set; }
    }

    public class BillingStatementCustomer
    {
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public Address BillingAddress { get; set; }
    }
}
