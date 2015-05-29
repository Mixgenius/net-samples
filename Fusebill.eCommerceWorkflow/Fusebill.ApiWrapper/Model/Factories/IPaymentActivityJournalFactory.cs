using Model.Internal;
using Model.PaymentMethodValidation.Response;

namespace Model.Factories
{
    public interface IPaymentActivityJournalFactory
    {
        PaymentActivityJournal Create(BaseResponse baseResponse, PaymentSource paymentSource, PaymentType paymentType, decimal amount, long customerId, long currencyId, PaymentMethodType paymentMethodType, long? paymentMethodId = null);
        PaymentActivityJournal Create(BaseResponse baseResponse, bool storedInFusebillVault, PaymentSource paymentSource, PaymentType paymentType, decimal amount, long customerId, long currencyId, InvoiceCollectOptions invoiceCollectOptions, bool hasDefaultPaymentMethod);
        PaymentActivityJournal Create(Payment payment, string paymentMethodType);
        PaymentActivityJournal Create(Refund refund, PaymentMethodType paymentMethodType);
    }
}
