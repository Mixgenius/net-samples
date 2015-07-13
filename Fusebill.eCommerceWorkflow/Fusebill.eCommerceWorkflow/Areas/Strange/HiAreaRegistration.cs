using System.Web.Mvc;

namespace Fusebill.eCommerceWorkflow.Areas.Hi
{
    public class HiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Hi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Hi_default",
                "Hi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
