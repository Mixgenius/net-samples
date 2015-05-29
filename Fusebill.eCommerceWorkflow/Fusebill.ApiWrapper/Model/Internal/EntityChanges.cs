using System.Collections.Generic;

namespace Model.Internal
{
    public class EntityChanges
    {
        public int? NumberAffected { get; set; }
        public int? NumberWillBeAffected { get; set; }
        public List<Change> Changes { get; set; }
    }
}
