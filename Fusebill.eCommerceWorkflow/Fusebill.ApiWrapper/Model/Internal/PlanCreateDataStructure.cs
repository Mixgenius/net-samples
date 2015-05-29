using System.Collections.Generic;

namespace Model.Internal
{
    public class PlanCreateDataStructure
    {
        public Plan Plan { get; set; }
        public List<PlanFrequency> PlanFrequencies { get; set; }
    }
}
