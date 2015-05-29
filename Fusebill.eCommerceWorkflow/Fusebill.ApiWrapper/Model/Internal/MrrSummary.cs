using System;

namespace Model.Internal
{
    public class MrrSummary : MrrData
    {
        public DateTime AsOfDate { get; set; }

        public MrrData MonthToDate { get; set; }
    }
}
