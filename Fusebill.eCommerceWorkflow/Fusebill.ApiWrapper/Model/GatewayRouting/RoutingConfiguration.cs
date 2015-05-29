using System.Collections.Generic;

namespace Model.GatewayRouting
{
    public class RoutingConfiguration
    {
        public int AccountRoutingConfigurationId { get; set; }
        public int AccountId { get; set; }
        public List<Route> Routes { get; set; }
    }
}
