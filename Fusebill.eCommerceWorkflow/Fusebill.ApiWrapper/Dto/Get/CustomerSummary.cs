using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class CustomerSummary : BaseDto
    {
        [JsonProperty(PropertyName = "fusebillNumber")]
        [DisplayName("Fusebill ID")]
        public long FusebillNumber { get; set; }

        [JsonProperty(PropertyName = "reference")]
        [DisplayName("Customer ID")]
        public string CustomerReference { get; set; }

        [JsonProperty(PropertyName = "companyName")]
        [DisplayName("Company name")]
        public string CompanyName { get; set; }

        [JsonProperty(PropertyName = "title")]
        [DisplayName("Title")]
        public string CustomerTitle { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        [DisplayName("First name")]
        public string CustomerFirstName { get; set; }

        [JsonProperty(PropertyName = "middleName")]
        [DisplayName("Middle name")]
        public string CustomerMiddleName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        [DisplayName("Last name")]
        public string CustomerLastName { get; set; }

        [JsonProperty(PropertyName = "suffix")]
        [DisplayName("Suffix")]
        public string CustomerSuffix { get; set; }

        [JsonProperty(PropertyName = "status")]
        [DisplayName("Status")]
        public String Status { get; set; }

        [JsonProperty(PropertyName = "netTerms")]
        [DisplayName("Net Terms")]
        public String NetTerms { get; set; }

        [JsonProperty(PropertyName = "paymentMethod")]
        [DisplayName("Payment Method")]
        public String PaymentMethod { get; set; }
        
        [JsonProperty(PropertyName = "sortStatus")]
        [DisplayName("Sort Status")]
        public String SortStatus { get; set; }

        [JsonProperty(PropertyName = "accountingStatus")]
        [DisplayName("Accounting Status")]
        public String AccountingStatus { get; set; }

        [JsonProperty(PropertyName = "arBalance")]
        [DisplayName("AR balance")]
        public decimal? ArBalance { get; set; }

        [JsonProperty(PropertyName = "createdTimestamp")]
        [DisplayName("Created")]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "nextBillingDate")]
        [DisplayName("Next Billing Date")]
        public Nullable<DateTime> NextBillingDate { get; set; }

        [JsonProperty(PropertyName = "currencyId")]
        [DisplayName("CurrencyId")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "daysUntilSuspension")]
        [DisplayName("Days Until Suspension")]
        public int? DaysUntilSuspension { get; set; }

        [JsonProperty(PropertyName = "primaryEmail")]
        [DisplayName("Primary Email")]
        public string PrimaryEmail { get; set; }

        [JsonProperty(PropertyName = "primaryPhone")]
        [DisplayName("Primary Phone")]
        public string PrimaryPhone { get; set; }

        [JsonProperty(PropertyName = "secondaryEmail")]
        [DisplayName("Secondary Email")]
        public string SecondaryEmail { get; set; }

        [JsonProperty(PropertyName = "secondaryPhone")]
        [DisplayName("Secondary Phone")]
        public string SecondaryPhone { get; set; }

        [JsonProperty(PropertyName = "reference1")]
        [DisplayName("Reference1")]
        public string Reference1 { get; set; }

        [JsonProperty(PropertyName = "reference2")]
        [DisplayName("Reference2")]
        public string Reference2 { get; set; }

        [JsonProperty(PropertyName = "reference3")]
        [DisplayName("Reference3")]
        public string Reference3 { get; set; }

        [JsonProperty(PropertyName = "adContent")]
        [DisplayName("Ad Content")]
        public string AdContent { get; set; }

        [JsonProperty(PropertyName = "campaign")]
        [DisplayName("Campaign")]
        public string Campaign { get; set; }

        [JsonProperty(PropertyName = "keyword")]
        [DisplayName("Keyword")]
        public string Keyword { get; set; }

        [JsonProperty(PropertyName = "landingPage")]
        [DisplayName("Landing Page")]
        public string LandingPage { get; set; }

        [JsonProperty(PropertyName = "medium")]
        [DisplayName("Medium")]
        public string Medium { get; set; }

        [JsonProperty(PropertyName = "source")]
        [DisplayName("Source")]
        public string Source { get; set; }
       
        [JsonProperty(PropertyName = "id")]
        public new long Id { get; set; }
       
        [JsonProperty(PropertyName = "uri")]
        public new string Uri { get; set; }

        [JsonProperty(PropertyName = "monthlyRecurringRevenue")]
        [DisplayName("Monthly Recurring Revenue")]
        public decimal MonthlyRecurringRevenue { get; set; }

        [JsonProperty(PropertyName = "netMonthlyRecurringRevenue")]
        [DisplayName("Net MRR")]
        public decimal NetMonthlyRecurringRevenue { get; set; }

        [JsonProperty(PropertyName = "salesforceId")]
        [DisplayName("Salesforce Id")]
        public string SalesforceId { get; set; }

        [JsonProperty(PropertyName = "netsuiteId")]
        [DisplayName("Netsuite Id")]
        public string NetsuiteId { get; set; }

        [JsonProperty(PropertyName = "salesTrackingCode1Code")]
        [DisplayName("Sales Tracking Code 1 Code")]
        public string SalesTrackingCode1Code { get; set; }

        [JsonProperty(PropertyName = "salesTrackingCode1Name")]
        [DisplayName("Sales Tracking Code 1 Name")]
        public string SalesTrackingCode1Name { get; set; }

        [JsonProperty(PropertyName = "salesTrackingCode2Code")]
        [DisplayName("Sales Tracking Code 2 Code")]
        public string SalesTrackingCode2Code { get; set; }

        [JsonProperty(PropertyName = "salesTrackingCode2Name")]
        [DisplayName("Sales Tracking Code 2 Name")]
        public string SalesTrackingCode2Name { get; set; }

        [JsonProperty(PropertyName = "salesTrackingCode3Code")]
        [DisplayName("Sales Tracking Code 3 Code")]
        public string SalesTrackingCode3Code { get; set; }

        [JsonProperty(PropertyName = "salesTrackingCode3Name")]
        [DisplayName("Sales Tracking Code 3 Name")]
        public string SalesTrackingCode3Name { get; set; }

        [JsonProperty(PropertyName = "salesTrackingCode4Code")]
        [DisplayName("Sales Tracking Code 4 Code")]
        public string SalesTrackingCode4Code { get; set; }

        [JsonProperty(PropertyName = "salesTrackingCode4Name")]
        [DisplayName("Sales Tracking Code 4 Name")]
        public string SalesTrackingCode4Name { get; set; }

        [JsonProperty(PropertyName = "salesTrackingCode5Code")]
        [DisplayName("Sales Tracking Code 5 Code")]
        public string SalesTrackingCode5Code { get; set; }

        [JsonProperty(PropertyName = "salesTrackingCode5Name")]
        [DisplayName("Sales Tracking Code 5 Name")]
        public string SalesTrackingCode5Name { get; set; }
    }
}
