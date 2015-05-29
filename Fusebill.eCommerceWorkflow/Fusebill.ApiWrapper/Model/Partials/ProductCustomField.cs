using Model.Internal;
using Newtonsoft.Json;

namespace Model
{
    public partial class ProductCustomField : IDefaultCustomField
    {
        [JsonIgnore]
        public dynamic DefaultValue { get; set; }
    }
}
