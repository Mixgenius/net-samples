using System.Collections.Generic;

namespace Model.Internal
{
    public static class DefaultSalesTrackingCodeConfiguration
    {
        public static Dictionary<SalesTrackingCodeType, string> Configurations
        {
            get
            {
                return new Dictionary<SalesTrackingCodeType, string>
                {
                    { SalesTrackingCodeType.SalesTrackingCode1, "Sales Tracking Code 1" },
                    { SalesTrackingCodeType.SalesTrackingCode2, "Sales Tracking Code 2" },
                    { SalesTrackingCodeType.SalesTrackingCode3, "Sales Tracking Code 3" },
                    { SalesTrackingCodeType.SalesTrackingCode4, "Sales Tracking Code 4" },
                    { SalesTrackingCodeType.SalesTrackingCode5, "Sales Tracking Code 5" }
                };
            }
        }
    }
}
