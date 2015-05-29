using System;
using System.Collections.Generic;
using Model.Internal;

namespace Model
{
    public interface ISideEffects
    {
        void Rebirth();

        List<DraftCharge> DraftCharges { get; }
        List<DraftInvoice> DraftInvoices { get; }
        List<Invoice> Invoices { get; }
        List<Invoice> NewInvoices { get; }
        List<PaymentActivityJournal> PaymentActivityJournals { get; }
        List<Subscription> SubscriptionActivations { get; }
        List<long> CustomersWithDraftInvoicesToDelete { get; }
        List<long> CustomersMovingToSuspendedStatus { get; }
        List<CustomerBillingSetting> CustomersToUpdateServiceStatus { get; }
        List<EntityInfo> EntitiesToJournalSubscriptionProducts { get; }

        void AppendDraftCharge(DraftCharge draftCharge);

        void AppendDraftInvoice(DraftInvoice draftInvoice);
        void AppendDraftInvoices(List<DraftInvoice> draftInvoices);
        void ResetDraftInvoices();

        void AppendInvoice(Invoice invoice);
        void AppendInvoices(List<Invoice> invoices);

        void AppendNewInvoice(Invoice invoice);
        void ResetNewInvoices();

        void AppendPaymentActivityJournal(PaymentActivityJournal paymentActivityJournal);
        void ResetPaymentActivityJournals();

        void AppendSubscriptionActivation(Subscription subscription);
        void AppendCustomerMovingToSuspendedStatus(long customerId);
        void ResetSubscriptionActivations();
        void ResetCustomerSuspensions();

        void AppendCustomerNeedingDraftInvoiceDeletion(long customerId);
        void AppendEntityToJournalSubscriptionProduct(EntityType entity, long id, EntityType? parentEntityType, long? parentId);
        void ResetEntitiesToJournalSubscriptionProducts();

        void AppendCustomerToUpdateServiceStatus(CustomerBillingSetting customerBillingSetting);
        void ResetCustomersToUpdateServiceStatus();
    }
}
