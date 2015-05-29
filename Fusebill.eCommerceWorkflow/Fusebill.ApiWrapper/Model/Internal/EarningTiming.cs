namespace Model.Internal
{
    public class EarningTiming
    {
        public Interval? EarningInterval { get; set; }

        public int? EarningNumberOfInterval { get; set; }

        public EarningTimingInterval EarningTimingInterval { get; set; }

        public EarningTimingType EarningTimingType { get; set; }
    }
}