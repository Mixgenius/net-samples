using System.Collections.Generic;

namespace Model
{
    public partial class AccountSalesTrackingCodeConfiguration
    {
        public Dictionary<SalesTrackingCodeType, string> SalesTrackingCodeConfigurations
        {
            get
            {
                return new Dictionary<SalesTrackingCodeType, string>
                {
                    { SalesTrackingCodeType.SalesTrackingCode1, SalesTrackingCode1Label },
                    { SalesTrackingCodeType.SalesTrackingCode2, SalesTrackingCode2Label },
                    { SalesTrackingCodeType.SalesTrackingCode3, SalesTrackingCode3Label },
                    { SalesTrackingCodeType.SalesTrackingCode4, SalesTrackingCode4Label },
                    { SalesTrackingCodeType.SalesTrackingCode5, SalesTrackingCode5Label }
                };
            }
        }
    }
}
