namespace Model.Internal
{
    public class BillingOptions
    {
        public bool ProrateRenewal { get; set; }
        public bool ChargeOnPreviousPeriod { get; set; }
        public bool StartServiceDateIsNow { get; set; }
    }
}
