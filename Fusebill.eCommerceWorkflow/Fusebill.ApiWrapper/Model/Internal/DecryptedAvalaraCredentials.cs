namespace Model.Internal
{
    public class DecryptedAvalaraCredentials
    {
        public DecryptedAvalaraCredentials(string accountNumber, string licenseKey)
        {
            AccountNumber = accountNumber;
            LicenseKey = licenseKey;
        }

        public string AccountNumber { get; private set; }
        public string LicenseKey { get; private set; }
    }
}
