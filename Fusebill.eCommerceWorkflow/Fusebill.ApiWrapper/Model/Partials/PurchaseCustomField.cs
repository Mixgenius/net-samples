using Model.Internal;
using Newtonsoft.Json;

namespace Model
{
    public partial class PurchaseCustomField : ICustomField
    {
        [JsonIgnore]
        public string Key { get; set; }

        [JsonIgnore]
        public dynamic Value { get; set; }
    }
}
