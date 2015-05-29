using System.Collections.Generic;

namespace Model.GatewayRouting
{
    public class Route
    {
        public int Priority { get; set; }
        public int AccountConfigurationId { get; set; }

        public List<CardTypeRule> CardTypeRules { get; set; }
        public List<CurrencyTypeRule> CurrencyTypeRules { get; set; }
    }
}