using DataCommon.Models;

namespace Model
{
    public class CustomerCredentialWrapper
    {
        public long CustomerId { get; set; }
        public CustomerCredential CustomerCredential { get; set; }
        public CustomerCredentialStatus CustomerCredentialStatus { get; set; }
        public string PortalLink { get; set; }
    }
}
