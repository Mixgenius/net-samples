using System.Web.Mvc;

namespace Fusebill.eCommerceWorkflow.Areas.Samples
{
    public class SamplesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Samples";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Samples_default",
                "Samples/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
