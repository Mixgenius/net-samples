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
    
    public partial class Purchase : Entity
    {
        public Purchase()
        {
            this.PurchaseCharges = new HashSet<PurchaseCharge>();
            this.PurchaseCouponCodes = new HashSet<PurchaseCouponCode>();
            this.PurchaseCustomFields = new HashSet<PurchaseCustomField>();
            this.PurchaseDiscounts = new HashSet<PurchaseDiscount>();
            this.PurchasePriceRanges = new HashSet<PurchasePriceRange>();
            this.DraftCharges = new HashSet<DraftCharge>();
            this.ProductItems = new HashSet<ProductItem>();
        }
    
        public long ProductId { get; set; }
        public PurchaseStatus Status { get; set; }
        public long CustomerId { get; set; }
        public decimal Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime CreatedTimestamp { get; set; }
        public System.DateTime ModifiedTimestamp { get; set; }
        public System.DateTime EffectiveTimestamp { get; set; }
        public Nullable<System.DateTime> PurchaseTimestamp { get; set; }
        public PricingModelType PricingModelType { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxableAmount { get; set; }
        public bool IsEarnedImmediately { get; set; }
        public Nullable<Interval> EarningInterval { get; set; }
        public Nullable<int> EarningNumberOfInterval { get; set; }
        public bool IsTrackingItems { get; set; }
        public EarningTimingType EarningTimingType { get; set; }
        public EarningTimingInterval EarningTimingInterval { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<PurchaseCharge> PurchaseCharges { get; set; }
        public virtual ICollection<PurchaseCouponCode> PurchaseCouponCodes { get; set; }
        public virtual ICollection<PurchaseCustomField> PurchaseCustomFields { get; set; }
        public virtual ICollection<PurchaseDiscount> PurchaseDiscounts { get; set; }
        public virtual ICollection<PurchasePriceRange> PurchasePriceRanges { get; set; }
        public virtual ICollection<DraftCharge> DraftCharges { get; set; }
        public virtual ICollection<ProductItem> ProductItems { get; set; }
    }
}
