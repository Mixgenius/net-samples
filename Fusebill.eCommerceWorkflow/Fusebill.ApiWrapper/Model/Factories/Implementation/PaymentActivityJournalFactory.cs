using System;
using DataCommon.Models;
using Model.Internal;
using Model.PaymentMethodValidation.Response;

namespace Model.Factories.Implementation
{
    public class PaymentActivityJournalFactory : IPaymentActivityJournalFactory
    {
        private readonly ITimeOfTransaction _timeOfTransaction;
        
        public PaymentActivityJournalFactory(ITimeOfTransaction timeOfTransaction)
        {
            _timeOfTransaction = timeOfTransaction;
        }

        public PaymentActivityJournal Create(BaseResponse baseResponse, PaymentSource paymentSource, PaymentType paymentType, decimal amount, long customerId, long currencyId, PaymentMethodType paymentMethodType, long? paymentMethodId = null)
        {
            var paymentJournal = new PaymentActivityJournal
            {
                Amount = amount,
                AuthorizationCode = baseResponse.TransactionReferenceNumber,
                AuthorizationResponse = baseResponse.GatewayResponseDescription,
                PaymentPlatformCode = baseResponse.SuccessfulGatewayTransactionId,
                CustomerId = customerId,
                PaymentActivityStatus = (baseResponse.Success) ? PaymentActivityStatus.Successful : PaymentActivityStatus.Failed,
                EffectiveTimestamp = _timeOfTransaction.Timestamp,
                PaymentSource = paymentSource,
                PaymentType = paymentType,
                CurrencyId = currencyId,
                GatewayId = baseResponse.AccountConfigurationId,
                GatewayName = baseResponse.AccountConfigurationName,
                PaymentMethodId = paymentMethodId,
                PaymentMethodType = paymentMethodType
            };

            if (!string.IsNullOrEmpty(baseResponse.SecondaryTransactionReferenceNumber))
            {
                paymentJournal.SecondaryTransactionNumber = baseResponse.SecondaryTransactionReferenceNumber;
                //Trim to database field size just in case, would not want to fail to write the record
                if (paymentJournal.SecondaryTransactionNumber.Length > 255)
                {
                    paymentJournal.SecondaryTransactionNumber = paymentJournal.SecondaryTransactionNumber.Substring(0, 255);
                }
            }

            return paymentJournal;
        }

        public PaymentActivityJournal Create(BaseResponse baseResponse, bool storedInFusebillVault, PaymentSource paymentSource, PaymentType paymentType,
            decimal amount, long customerId, long currencyId, InvoiceCollectOptions invoiceCollectOptions, bool hasDefaultPaymentMethod)
        {
            var paymentMethodType = invoiceCollectOptions.PaymentMethod.CreditCard != null
                ? PaymentMethodType.CreditCard
                : PaymentMethodType.ACH;

            var paymentActivityJournal = Create(baseResponse, paymentSource, paymentType, amount, customerId, currencyId,
                paymentMethodType);

            if (baseResponse.Success && (
                invoiceCollectOptions.PaymentMethodOptions == PaymentMethodOptions.UseProvidedPaymentMethodAndSave || 
                    invoiceCollectOptions.PaymentMethodOptions == PaymentMethodOptions.UseProvidedPaymentMethodAndMakeDefault))
            {
                if (invoiceCollectOptions.PaymentMethod.CreditCard != null)
                    paymentActivityJournal.PaymentMethod = new CreditCard
                    {
                        AccountType = invoiceCollectOptions.PaymentMethod.CreditCard.CardShortCode,
                        FirstName = invoiceCollectOptions.PaymentMethod.CreditCard.FirstName,
                        LastName = invoiceCollectOptions.PaymentMethod.CreditCard.LastName,
                        MaskedCardNumber = invoiceCollectOptions.PaymentMethod.CreditCard.Number.Substring(invoiceCollectOptions.PaymentMethod.CreditCard.Number.Length - 4),
                        PaymentMethodStatus = PaymentMethodStatus.Active,
                        PaymentMethodType = PaymentMethodType.CreditCard,
                        ExpirationMonth = invoiceCollectOptions.PaymentMethod.CreditCard.ExpirationDate.Month,
                        ExpirationYear = invoiceCollectOptions.PaymentMethod.CreditCard.ExpirationDate.Year
                    };
                else
                    paymentActivityJournal.PaymentMethod = new AchCard
                    {
                        AccountType = invoiceCollectOptions.PaymentMethod.AchAccount.AccountType,
                        FirstName = invoiceCollectOptions.PaymentMethod.AchAccount.FirstName,
                        LastName = invoiceCollectOptions.PaymentMethod.AchAccount.LastName,
                        MaskedAccountNumber = invoiceCollectOptions.PaymentMethod.AchAccount.AccountNumber.Substring(invoiceCollectOptions.PaymentMethod.AchAccount.AccountNumber.Length - 4),
                        MaskedTransitNumber = invoiceCollectOptions.PaymentMethod.AchAccount.TransitNumber.Substring(invoiceCollectOptions.PaymentMethod.AchAccount.TransitNumber.Length - 4),
                        PaymentMethodStatus = PaymentMethodStatus.Active,
                        PaymentMethodType = PaymentMethodType.ACH
                    };

                paymentActivityJournal.PaymentMethod.CustomerId = customerId;
                paymentActivityJournal.PaymentMethod.Token = baseResponse.Token;
                paymentActivityJournal.PaymentMethod.StoredInFusebillVault = storedInFusebillVault;
                
                if (baseResponse.ExternalVault != null)
                {
                    paymentActivityJournal.PaymentMethod.ExternalCardId = baseResponse.ExternalVault.CardId;
                    paymentActivityJournal.PaymentMethod.ExternalCustomerId = baseResponse.ExternalVault.CustomerId;
                }
                paymentActivityJournal.PaymentMethod.IsDefault = !hasDefaultPaymentMethod;

                ConvertAddress(invoiceCollectOptions, paymentActivityJournal);
            }

            return paymentActivityJournal;
        }

        private static void ConvertAddress(InvoiceCollectOptions invoiceCollectOptions,
            PaymentActivityJournal paymentActivityJournal)
        {
            if (invoiceCollectOptions.Address != null && 
                invoiceCollectOptions.Address.Line1 != null && invoiceCollectOptions.Address.Line2 != null && 
                    invoiceCollectOptions.Address.City != null && invoiceCollectOptions.Address.PostalZip != null)
            {
                paymentActivityJournal.PaymentMethod.Address1 = invoiceCollectOptions.Address.Line1;
                paymentActivityJournal.PaymentMethod.Address2 = invoiceCollectOptions.Address.Line2;
                paymentActivityJournal.PaymentMethod.City = invoiceCollectOptions.Address.City;
                paymentActivityJournal.PaymentMethod.CountryId = invoiceCollectOptions.Address.CountryId;
                paymentActivityJournal.PaymentMethod.PostalZip = invoiceCollectOptions.Address.PostalZip;
                paymentActivityJournal.PaymentMethod.StateId = invoiceCollectOptions.Address.StateId;
            }
        }

        public PaymentActivityJournal Create(Payment payment, string paymentMethodType)
        {
            return new PaymentActivityJournal
            {
                Amount = payment.Amount,
                CurrencyId = payment.CurrencyId,
                Customer = payment.Customer,
                PaymentActivityStatus = PaymentActivityStatus.Successful,
                EffectiveTimestamp = _timeOfTransaction.Timestamp,
                PaymentSource = PaymentSource.Manual,
                PaymentType = PaymentType.Collect,
                PaymentMethodType = (PaymentMethodType)Enum.Parse(typeof(PaymentMethodType), paymentMethodType)
            };
        }

        public PaymentActivityJournal Create(Refund refund, PaymentMethodType paymentMethodType)
        {
            return new PaymentActivityJournal
            {
                Amount = refund.Amount,
                CurrencyId = refund.CurrencyId,
                Customer = refund.Customer,
                PaymentActivityStatus = PaymentActivityStatus.Successful,
                EffectiveTimestamp = _timeOfTransaction.Timestamp,
                PaymentSource = PaymentSource.Manual,
                PaymentType = PaymentType.Refund,
                PaymentMethodType = paymentMethodType
            };
        }
    }
}
