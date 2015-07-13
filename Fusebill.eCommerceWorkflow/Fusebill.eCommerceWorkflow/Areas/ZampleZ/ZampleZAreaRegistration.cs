using System.Web.Mvc;

namespace Fusebill.eCommerceWorkflow.Areas.ZampleZ
{
    public class ZampleZAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ZampleZ";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ZampleZ_default",
                "ZampleZ/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
