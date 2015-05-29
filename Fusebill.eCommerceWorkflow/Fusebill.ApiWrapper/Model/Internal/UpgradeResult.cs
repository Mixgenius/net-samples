namespace Model.Internal
{
    public class UpgradeResult
    {
        public bool Success { get; set; }
        public EntityChanges AllSubscriptions { get; set; }
        public EntityChanges ExistingSubscriptions { get; set; }
    }
}
