using System;
using System.Collections.Generic;
using System.Linq;
using DataCommon.Models;
using Model.Avalara;
using Model.Internal;

namespace Model.FactoryMethods
{
    public class AvalaraFactoryMethods
    {
        public static GetTaxRequest GetTaxRequest(Account account, Address destinationAddress, ICollection<DraftCharge> draftCharges, bool commitTransaction, string currency, DateTime effectiveDate, string poNumber)
        {
            var customerBillingSetting = draftCharges.First().DraftInvoice.Customer.CustomerBillingSetting;

            var isCustomerTaxExempt = customerBillingSetting.TaxExempt;
            var exemptionCode = customerBillingSetting.TaxExemptCode;
            var customerUsageType = customerBillingSetting.AvalaraUsageType;
            var vatIdentificationNumber = customerBillingSetting.VATIdentificationNumber;

            var taxRequest = GetTaxRequest(account, destinationAddress, DocType.SalesOrder, commitTransaction, currency, isCustomerTaxExempt,
                exemptionCode, Guid.NewGuid().ToString(), poNumber, effectiveDate, customerUsageType, string.Empty, vatIdentificationNumber);
            
            SetLines(taxRequest, isCustomerTaxExempt, draftCharges, customerUsageType);

            return taxRequest;
        }

        public static GetTaxRequest GetTaxRequest(Account account, Address destinationAddress, ICollection<Charge> charges, bool commitTransaction, Guid avalaraId, string currency, DateTime effectiveDate, string poNumber)
        {
            var customerBillingSetting = charges.First().Invoice.Customer.CustomerBillingSetting;

            var isCustomerTaxExempt = customerBillingSetting.TaxExempt;
            var exemptionCode = customerBillingSetting.TaxExemptCode;
            var customerUsageType = customerBillingSetting.AvalaraUsageType;
            var vatIdentificationNumber = customerBillingSetting.VATIdentificationNumber;

            var taxRequest = GetTaxRequest(account, destinationAddress, DocType.SalesInvoice, commitTransaction, currency, isCustomerTaxExempt,
                exemptionCode, avalaraId.ToString(), poNumber, effectiveDate, customerUsageType, string.Empty, vatIdentificationNumber);
            
            SetLines(taxRequest, isCustomerTaxExempt, charges, customerUsageType);

            return taxRequest;
        }

        public static GetTaxRequest GetReverseTaxRequest(Account account, ReverseChargeResults reverseChargeResults, Invoice invoice, string currency)
        {
            var destinationAddress = account.AccountFeatureConfiguration.UseBillingAddressForTaxes()
                ? invoice.Customer.CustomerAddressPreference.BillingAddress
                : invoice.Customer.CustomerAddressPreference.ShippingAddressWithFallBack;

            var customerBillingSetting = invoice.Customer.CustomerBillingSetting;

            var isCustomerTaxExempt = customerBillingSetting.TaxExempt;
            var exemptionCode = customerBillingSetting.TaxExemptCode;
            var customerUsageType = customerBillingSetting.AvalaraUsageType;
            var vatIdentificationNumber = customerBillingSetting.VATIdentificationNumber;

            var reversedCharge = reverseChargeResults.UnearnedReverseCharge ?? reverseChargeResults.EarnedReverseCharge;

            var taxRequest = GetTaxRequest(account, destinationAddress, DocType.ReturnInvoice, true, currency, isCustomerTaxExempt,
                exemptionCode, Guid.NewGuid().ToString(), invoice.PoNumber, reversedCharge.EffectiveTimestamp, customerUsageType, invoice.AvalaraId.GetValueOrDefault().ToString(), vatIdentificationNumber);

            SetLines(taxRequest, isCustomerTaxExempt, reverseChargeResults, customerUsageType, invoice.EffectiveTimestamp);

            return taxRequest;
        }

        public static GetTaxRequest GetTestTaxRequestForValidation(AvalaraConfiguration avalaraConfiguration)
        {
            var taxRequest = new GetTaxRequest
            {
                Addresses = GetAddresses(avalaraConfiguration, new Address
                {
                    Line1 = "232 Herzberg Rd",
                    City = "Ottawa",
                    PostalZip = "K2K 2A1",
                    Country = new Country
                    {
                        ISO = "CA"
                    },
                    State = new State
                    {
                        SubdivisionISOCode = "ON"
                    }
                }),
                CompanyCode = avalaraConfiguration.OrganizationCode,
                CustomerCode = string.Format("{0}-test", avalaraConfiguration.Id),
                DocDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                DocType = DocType.SalesOrder
            };

            SetLines(taxRequest, false, new List<Charge>
            {
                new Charge
                {
                    Amount = 10m,
                    Name = "Test",
                    Quantity = 1
                }
            }, "G");

            return taxRequest;
        }

        private static GetTaxRequest GetTaxRequest(Account account, Address destinationAddress, DocType docType, bool commitTransaction, string currency, bool isCustomerTaxExempt, string exemptionCode, string avalaraId, string poNumber, DateTime effectiveDate, string customerUsageType, string referenceCode, string vatIdentificationNumber)
        {
            var taxRequest = new GetTaxRequest
            {
                Addresses = GetAddresses(account.AvalaraConfiguration, destinationAddress),
                Client = "Fusebill General Integration",
                Commit = commitTransaction,
                CompanyCode = account.AccountFeatureConfiguration.GetOrganizationCode(),
                CurrencyCode = currency,
                CustomerCode = string.Format("{0}-{1}", account.Id, destinationAddress.CustomerAddressPreferenceId),
                CustomerUsageType = customerUsageType,
                ExemptionNo = isCustomerTaxExempt ? "Exempt " + exemptionCode : string.Empty,
                DetailLevel = DetailLevel.Tax,
                DocCode = avalaraId,
                DocDate = effectiveDate.ToString("yyyy-MM-dd"),
                DocType = docType,
                PurchaseOrderNo = poNumber,
                ReferenceCode = referenceCode,
                BusinessIdentificationNo = vatIdentificationNumber
            };

            if (isCustomerTaxExempt)
            {
                taxRequest.TaxOverride = new TaxOverrideDef
                {
                    TaxAmount = "0",
                    TaxOverrideType = "TaxAmount",
                    Reason = "Exempt " + exemptionCode
                };
            }

            return taxRequest;
        }

        private static Avalara.Address[] GetAddresses(AvalaraConfiguration avalaraConfiguration, Address destinationAddress)
        {
            Avalara.Address organizationAddress = null;

            if (avalaraConfiguration != null && avalaraConfiguration.CountryId.HasValue)
            {
                organizationAddress = new Avalara.Address
                {
                    AddressCode = "01",
                    Line1 = avalaraConfiguration.Line1,
                    Line2 = avalaraConfiguration.Line2,
                    City = avalaraConfiguration.City,
                    Country = avalaraConfiguration.Country.ISO,
                    Region =
                        avalaraConfiguration.State != null
                            ? avalaraConfiguration.State.SubdivisionISOCode
                            : string.Empty,
                    PostalCode = avalaraConfiguration.PostalZip
                };
            }

            var address = new Avalara.Address
            {
                AddressCode = organizationAddress != null ? "02" : "01",
                Line1 = destinationAddress.Line1,
                Line2 = destinationAddress.Line2,
                City = destinationAddress.City,
                Country = destinationAddress.Country.ISO,
                Region = destinationAddress.State != null ? destinationAddress.State.SubdivisionISOCode : string.Empty,
                PostalCode = destinationAddress.PostalZip
            };

            if (organizationAddress != null)
                return new[] {organizationAddress, address};

            return new[] {address};
        }

        private static void SetLines(GetTaxRequest taxRequest, bool isCustomerTaxExempt, IEnumerable<DraftCharge> draftCharges, string customerUsageType)
        {
            var counter = 1;

            taxRequest.Lines =
                draftCharges.Select(
                    draftCharge =>
                    {
                        var product = draftCharge.IsDraftPurchaseCharge()
                            ? draftCharge.Purchase.Product
                            : draftCharge.IsDraftSubscriptionProductCharge()
                                ? draftCharge.DraftSubscriptionProductCharge.SubscriptionProduct.PlanProduct.Product
                                : new Product();

                        return GetLine(taxRequest, draftCharge.TaxableAmount, draftCharge.Name, draftCharge.Quantity,
                            counter++.ToString(), isCustomerTaxExempt, draftCharge.IsTaxExempt(), customerUsageType, 
                            product.AvalaraItemCode, product.AvalaraTaxCode);
                    }).ToArray();
        }

        private static void SetLines(GetTaxRequest taxRequest, bool isCustomerTaxExempt, IEnumerable<Charge> charges, string customerUsageType)
        {
            var counter = 1;

            taxRequest.Lines =
                charges.Select(
                    charge =>
                    {
                        var product = charge.PurchaseCharge != null
                            ? charge.PurchaseCharge.Purchase.Product
                            : charge.SubscriptionProductCharge != null
                                ? charge.SubscriptionProductCharge.SubscriptionProduct.PlanProduct.Product
                                : new Product();

                        return GetLine(taxRequest, charge.TaxableAmount(), charge.Name, charge.Quantity, counter++.ToString(),
                            isCustomerTaxExempt, charge.IsTaxExempt(), customerUsageType, product.AvalaraItemCode, product.AvalaraTaxCode);
                    }).ToArray();
        }

        private static void SetLines(GetTaxRequest taxRequest, bool isCustomerTaxExempt, ReverseChargeResults reverseChargeResults, string customerUsageType, DateTime effectiveDate)
        {
            var reversedCharge = reverseChargeResults.UnearnedReverseCharge ?? reverseChargeResults.EarnedReverseCharge;

            var product = reversedCharge.Charge.PurchaseCharge != null
                            ? reversedCharge.Charge.PurchaseCharge.Purchase.Product
                            : reversedCharge.Charge.SubscriptionProductCharge != null
                                ? reversedCharge.Charge.SubscriptionProductCharge.SubscriptionProduct.PlanProduct.Product
                                : new Product();

            var line = GetLine(taxRequest, reverseChargeResults.TaxableReversedAmountSum*-1, reversedCharge.Charge.Description,
                1, "1", isCustomerTaxExempt, reversedCharge.Charge.IsTaxExempt(), customerUsageType, product.AvalaraItemCode, product.AvalaraTaxCode);

            line.TaxOverride = new TaxOverrideDef
            {
                TaxOverrideType = "TaxDate",
                Reason = "Reversing charge",
                TaxDate = effectiveDate.ToString("yyyy-MM-dd") 
            };

            taxRequest.Lines = new[] { line };
        }

        private static Line GetLine(GetTaxRequest taxRequest, decimal taxableAmount, string description, decimal quantity, string lineNo, bool isCustomerTaxExempt, bool isChargeTaxExempt, string customerUsageType, string avalaraItemCode, string avalaraTaxCode)
        {
            var line = new Line
            {
                Amount = taxableAmount,
                OriginCode = taxRequest.Addresses.Count() > 1 ? "01" : string.Empty,
                DestinationCode = taxRequest.Addresses.Count() > 1 ? "02" : "01",
                Description = description,
                Qty = quantity,
                LineNo = lineNo,
                TaxCode = string.IsNullOrEmpty(avalaraTaxCode) ? "O0000000" : avalaraTaxCode,
                ItemCode = avalaraItemCode,
                CustomerUsageType = customerUsageType
            };

            if (!isCustomerTaxExempt && isChargeTaxExempt)
            {
                line.TaxOverride = new TaxOverrideDef
                {
                    TaxAmount = "0",
                    TaxOverrideType = "TaxAmount",
                    Reason = "Product Exempt"
                };
            }

            return line;
        }

        public static void CreateDraftTaxesFromResult(Account account, List<Country> countries, ICollection<DraftCharge> draftCharges, Country billingAddressCountry, GetTaxResult taxResult)
        {
            var existingTaxRules = account.TaxRules;

            var counter = 0;
            foreach (var draftCharge in draftCharges)
            {
                var lineResult = taxResult.TaxLines[counter++];
                if (draftCharge.Status == DraftChargeStatus.Active)
                {
                    foreach (var taxDetail in lineResult.TaxDetails)
                    {
                        if (taxDetail.Tax > 0)
                        {
                            TaxRule existingTaxRule = null;

                            existingTaxRule = existingTaxRules.SingleOrDefault(
                                tr =>
                                    tr.Name == taxDetail.TaxName.Trim() &&
                                    tr.Percentage.ToString("0.00000000") == taxDetail.Rate.ToString("0.00000000") &&
                                    tr.Country.ISO == taxDetail.Country.Trim() &&
                                    (tr.State == null ||
                                     (tr.State != null && tr.State.SubdivisionISOCode == taxDetail.Region.Trim()))) ??
                                              CreateTaxRule(countries, existingTaxRules, account, taxDetail);

                            draftCharge.DraftTaxes.Add(CreateDraftTax(taxDetail, existingTaxRule, draftCharge));
                        }
                    }
                }
            }
        }

        public static void CreateTaxesFromResult(Account account, List<Country> countries, ICollection<Charge> charges, Country billingAddressCountry, GetTaxResult taxResult)
        {
            var existingTaxRules = account.TaxRules;

            var counter = 0;
            foreach (var charge in charges)
            {
                var lineResult = taxResult.TaxLines[counter++];
                foreach (var taxDetail in lineResult.TaxDetails)
                {
                    if (taxDetail.Tax > 0)
                    {
                        var existingTaxRule =
                        existingTaxRules.SingleOrDefault(
                            tr =>
                                tr.Name == taxDetail.TaxName.Trim() &&
                                tr.Percentage.ToString("0.00000000") == taxDetail.Rate.ToString("0.00000000") &&
                                tr.Country.ISO == taxDetail.Country.Trim() &&
                                (tr.State == null ||
                                 (tr.State != null && tr.State.SubdivisionISOCode == taxDetail.Region.Trim()))) ??
                        CreateTaxRule(countries, existingTaxRules, account, taxDetail);

                        charge.Taxes.Add(TaxFactoryMethods.CreateTax(taxDetail, existingTaxRule, charge));
                    }
                }
            }
        }

        public static void CreateReverseTaxesFromResult(Account account, ReverseChargeResults reverseChargeResults, GetTaxResult taxResult)
        {
            var existingTaxRules = account.TaxRules;

            var reversedCharge = reverseChargeResults.UnearnedReverseCharge ?? reverseChargeResults.EarnedReverseCharge;

            var lineResult = taxResult.TaxLines[0];

            foreach (var taxDetail in lineResult.TaxDetails)
            {
                var taxableAmount = Math.Abs(taxDetail.Tax);
                if (taxableAmount > 0)
                {
                    var existingTaxRule = existingTaxRules.SingleOrDefault(
                        tr =>
                            tr.Name == taxDetail.TaxName.Trim() &&
                            tr.Percentage.ToString("0.00000000") == taxDetail.Rate.ToString("0.00000000") &&
                            tr.Country.ISO == taxDetail.Country.Trim() &&
                            (tr.State == null ||
                             (tr.State != null && tr.State.SubdivisionISOCode == taxDetail.Region.Trim())));

                    if (null == existingTaxRule)
                        throw new FusebillException(string.Format("Unable to find matching tax rule for reversal. The rule {0} does not exist.", taxDetail.TaxName.Trim()));

                    var originalTax = reversedCharge.Charge.Taxes.SingleOrDefault(t => t.TaxRuleId == existingTaxRule.Id);
                    if (null == originalTax)
                        throw new FusebillException(string.Format("Unable to find original tax named {0} to perform reversal.", taxDetail.TaxName.Trim()));

                    TaxFactoryMethods.CreateReverseTax(originalTax, reversedCharge, reverseChargeResults.CreditNote,
                        taxableAmount, reversedCharge.EffectiveTimestamp);
                }
            }
        }

        private static DraftTax CreateDraftTax(TaxDetail taxDetail, TaxRule existingTaxRule, DraftCharge draftCharge)
        {
            var draftTax = new DraftTax
            {
                Amount = taxDetail.Tax,
                TaxRule = existingTaxRule,
                CurrencyId = draftCharge.CurrencyId,
            };

            draftCharge.DraftInvoice.DraftTaxes.Add(draftTax);

            return draftTax;
        }

        private static TaxRule CreateTaxRule(List<Country> countries, ICollection<TaxRule> existingTaxRules, Account account, TaxDetail taxDetail)
        {
            var taxRule = new TaxRule
            {
                AccountId = account.Id,
                Country = countries.Single(c => c.ISO == taxDetail.Country),
                Name = taxDetail.TaxName.Trim(),
                Description = taxDetail.JurisName.Trim(),
                Percentage = taxDetail.Rate,
                State =
                    !string.IsNullOrEmpty(taxDetail.Region.Trim())
                        ? countries.Single(c => c.ISO == taxDetail.Country).States.SingleOrDefault(
                            s => s.SubdivisionISOCode == taxDetail.Region.Trim())
                        : null
            };

            existingTaxRules.Add(taxRule);

            return taxRule;
        }

        public static Avalara.Address GetAddressRequest(Address address)
        {
            return new Avalara.Address
            {
                Line1 = address.Line1,
                Line2 = address.Line2,
                City = address.City,
                Region = address.StateIso3,
                Country = address.CountryIso2,
                PostalCode = address.PostalZip
            };
        }

        public static Avalara.Address GetAddressRequest(AvalaraConfiguration avalaraConfiguration)
        {
            return new Avalara.Address
            {
                Line1 = avalaraConfiguration.Line1,
                Line2 = avalaraConfiguration.Line2,
                City = avalaraConfiguration.City,
                Region = avalaraConfiguration.State != null ? avalaraConfiguration.State.SubdivisionISOCode : string.Empty,
                Country = avalaraConfiguration.Country != null ? avalaraConfiguration.Country.ISO : string.Empty,
                PostalCode = avalaraConfiguration.PostalZip
            };
        }
    }
}
