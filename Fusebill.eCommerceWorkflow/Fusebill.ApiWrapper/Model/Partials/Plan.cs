using System.Linq;
using Newtonsoft.Json;

namespace Model
{
    public partial class Plan
    {
        [JsonIgnore]
        public PlanRevision ActivePlanRevision
        {
            get { return PlanRevisions.SingleOrDefault(r => r.IsActive); }
        }
    }
}
