using System;
using System.Collections.Generic;
using Model.Internal;

namespace Model.FactoryMethods
{
    public class BillingStatementFactoryMethods
    {
        public static BillingStatement Generate(Customer customer, string fullName, string signature, DateTime startDate, DateTime endDate, Tuple<decimal, decimal> openingBalance, string currency, bool associateCustomer)
        {
            return Generate(customer, fullName, signature, startDate, endDate,
                openingBalance.Item2 - openingBalance.Item1, currency, associateCustomer, 0);
        }

        public static BillingStatement Generate(Customer customer, string fullName, string signature, DateTime startDate,
            DateTime endDate, decimal openingBalance, string currency, bool associateCustomer, long existingId)
        {
            var billingStatement = new BillingStatement
            {
                Header = new BillingStatementHeader
                {
                    Customer = new BillingStatementCustomer
                    {
                        BillingAddress = customer.CustomerAddressPreference.BillingAddress,
                        CompanyName =
                            customer.CustomerAddressPreference.BillingAddress == null || string.IsNullOrEmpty(customer.CustomerAddressPreference.BillingAddress.CompanyName)
                                ? customer.CompanyName
                                : customer.CustomerAddressPreference.BillingAddress.CompanyName,
                        FullName = fullName
                    },
                    Signature = signature
                },
                StartDate = startDate,
                EndDate = endDate,
                Currency = currency,
                OpeningBalance = openingBalance,
                CustomerId = customer.Id,
                Id = existingId
            };

            if (associateCustomer)
                billingStatement.Customer = customer;

            return billingStatement;
        }

        public static BillingStatementSummary GenerateSummary(BillingStatement billingStatement, List<TransactionSummary> transactionSummaries)
        {
            return new BillingStatementSummary
            {
                Header = billingStatement.Header,
                StartDate = billingStatement.StartDate,
                EndDate = billingStatement.EndDate,
                OpeningBalance = billingStatement.OpeningBalance,
                ClosingBalance = billingStatement.ClosingBalance,
                Currency = billingStatement.Currency,
                Transactions = transactionSummaries,
                OriginalBillingStatement = billingStatement
            };
        }
    }
}
