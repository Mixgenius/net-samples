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
    
    public partial class ChargeProductItem : Entity
    {
        public long ChargeId { get; set; }
        public long ProductItemId { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
    
        public virtual Charge Charge { get; set; }
        public virtual ProductItem ProductItem { get; set; }
    }
}
