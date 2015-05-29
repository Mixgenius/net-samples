using System;
using System.Collections.Generic;
using Model.Internal;

namespace Model
{
    public partial class CustomerReference
    {
        public bool HasSalesTrackingCodes
        {
            get
            {
                return SalesTrackingCode1 != null || SalesTrackingCode2 != null || SalesTrackingCode3 != null || SalesTrackingCode4 != null || SalesTrackingCode5 != null;
            }
        }

        public List<Tuple<SalesTrackingCode, string>> SalesTrackingCodes { get; private set; }

        public void BuildSalesTrackingCodeListFromProperties(Dictionary<SalesTrackingCodeType, string> types)
        {
            var codes = new List<Tuple<SalesTrackingCode, string>>();

            if (SalesTrackingCode1 != null)
                codes.Add(new Tuple<SalesTrackingCode, string>(SalesTrackingCode1, types[SalesTrackingCode1.Type]));

            if (SalesTrackingCode2 != null)
                codes.Add(new Tuple<SalesTrackingCode, string>(SalesTrackingCode2, types[SalesTrackingCode2.Type]));

            if (SalesTrackingCode3 != null)
                codes.Add(new Tuple<SalesTrackingCode, string>(SalesTrackingCode3, types[SalesTrackingCode3.Type]));

            if (SalesTrackingCode4 != null)
                codes.Add(new Tuple<SalesTrackingCode, string>(SalesTrackingCode4, types[SalesTrackingCode4.Type]));

            if (SalesTrackingCode5 != null)
                codes.Add(new Tuple<SalesTrackingCode, string>(SalesTrackingCode5, types[SalesTrackingCode5.Type]));

            SalesTrackingCodes = codes;
        }

        public List<CustomerSalesTrackingCode> CustomerSalesTrackingCodesToSet { get; set; }
    }
}
