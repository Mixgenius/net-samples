//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Account : Entity
    {
        public Account()
        {
            this.AccountEmailTemplates = new HashSet<AccountEmailTemplate>();
            this.Products = new HashSet<Product>();
            this.TaxRules = new HashSet<TaxRule>();
            this.AccountEmailSchedules = new HashSet<AccountEmailSchedule>();
            this.AccountCurrencies = new HashSet<AccountCurrency>();
            this.AccountCollectionSchedules = new HashSet<AccountCollectionSchedule>();
            this.AccountUsers = new HashSet<AccountUser>();
            this.SalesforceOneTimeAuthorizationTokens = new HashSet<SalesforceOneTimeAuthorizationToken>();
            this.DiscountConfigurations = new HashSet<DiscountConfiguration>();
            this.CustomFields = new HashSet<CustomField>();
            this.Plans = new HashSet<Plan>();
            this.Customers = new HashSet<Customer>();
            this.Invoices = new HashSet<Invoice>();
            this.Coupons = new HashSet<Coupon>();
            this.CouponCodes = new HashSet<CouponCode>();
            this.CustomerCredential = new HashSet<CustomerCredential>();
            this.HostedPage = new HashSet<HostedPage>();
            this.FusebillSupportLogin = new HashSet<FusebillSupportLogin>();
            this.IntegrationSynchJob = new HashSet<IntegrationSyncJob>();
            this.SalesTrackingCodes = new HashSet<SalesTrackingCode>();
            this.Roles = new HashSet<Role>();
            this.AccountsExcludedFromBilling = new HashSet<AccountsExcludedFromBilling>();
            this.AccountsExcludedFromEarning = new HashSet<AccountsExcludedFromEarning>();
            this.AccountNetsuiteFieldMapping = new HashSet<AccountNetsuiteFieldMapping>();
            this.AvalaraLog = new HashSet<AvalaraLog>();
        }
    
        public System.DateTime CreatedTimestamp { get; set; }
        public string ContactEmail { get; set; }
        public string CompanyName { get; set; }
        public System.DateTime ModifiedTimestamp { get; set; }
        public Nullable<bool> FusebillTest { get; set; }
        public Nullable<bool> Signed { get; set; }
        public Nullable<bool> Live { get; set; }
        public string PublicApiKey { get; set; }
        public string OriginUrlForPublicApiKey { get; set; }
    
        public virtual ICollection<AccountEmailTemplate> AccountEmailTemplates { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<TaxRule> TaxRules { get; set; }
        public virtual ICollection<AccountEmailSchedule> AccountEmailSchedules { get; set; }
        public virtual AccountWebhooksKey AccountWebhooksKey { get; set; }
        public virtual ICollection<AccountCurrency> AccountCurrencies { get; set; }
        public virtual ICollection<AccountCollectionSchedule> AccountCollectionSchedules { get; set; }
        public virtual AccountPreference AccountPreference { get; set; }
        public virtual ICollection<AccountUser> AccountUsers { get; set; }
        public virtual ICollection<SalesforceOneTimeAuthorizationToken> SalesforceOneTimeAuthorizationTokens { get; set; }
        public virtual ICollection<DiscountConfiguration> DiscountConfigurations { get; set; }
        public virtual ICollection<CustomField> CustomFields { get; set; }
        public virtual AccountFeatureConfiguration AccountFeatureConfiguration { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual AccountBillingPreference AccountBillingPreference { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }
        public virtual ICollection<CouponCode> CouponCodes { get; set; }
        public virtual ICollection<CustomerCredential> CustomerCredential { get; set; }
        public virtual ICollection<HostedPage> HostedPage { get; set; }
        public virtual ICollection<FusebillSupportLogin> FusebillSupportLogin { get; set; }
        public virtual ICollection<IntegrationSyncJob> IntegrationSynchJob { get; set; }
        public virtual AccountSalesTrackingCodeConfiguration AccountSalesTrackingCodeConfiguration { get; set; }
        public virtual ICollection<SalesTrackingCode> SalesTrackingCodes { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual AvalaraConfiguration AvalaraConfiguration { get; set; }
        public virtual ICollection<AccountsExcludedFromBilling> AccountsExcludedFromBilling { get; set; }
        public virtual ICollection<AccountsExcludedFromEarning> AccountsExcludedFromEarning { get; set; }
        public virtual AccountBillingStatementPreference AccountBillingStatementPreference { get; set; }
        public virtual ICollection<AccountNetsuiteFieldMapping> AccountNetsuiteFieldMapping { get; set; }
        public virtual NetsuiteConfiguration NetsuiteConfiguration { get; set; }
        public virtual ICollection<AvalaraLog> AvalaraLog { get; set; }
    }
}
