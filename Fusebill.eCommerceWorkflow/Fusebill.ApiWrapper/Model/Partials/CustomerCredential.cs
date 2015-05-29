using DataCommon.Models;

namespace Model
{
    public partial class CustomerCredential
    {
        public CustomerCredentialStatus Status
        {
            get
            {
                if (Customer.Status.Equals(CustomerStatus.Draft) || Customer.Status.Equals(CustomerStatus.Cancelled))
                    return CustomerCredentialStatus.NotAvailable;
                if (string.IsNullOrEmpty(Password))
                    return CustomerCredentialStatus.NotCreated;
                return CustomerCredentialStatus.Created;
            }
        }
    }
}
