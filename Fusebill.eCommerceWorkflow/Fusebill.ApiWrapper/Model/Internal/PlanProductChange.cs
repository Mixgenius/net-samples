namespace Model.Internal
{
    public class PlanProductChange
    {
        public PlanProduct PlanProduct { get; set; }
        public long PlanId { get; set; }
        public PreviewResult PreviewResult { get; set; }
        public UpgradeResult UpgradeResult { get; set; }
    }
}
