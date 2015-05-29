using Fusebill.ApiWrapper;
using System.Configuration;
using System.Web.Mvc;

namespace Fusebill.eCommerceWorkflow.Controllers
{
    public class PlansController : FusebillController
    {
        //
        // GET: /Plans/
        public ActionResult Index()
        {
            var result = ApiClient.GetPlans(null);
            return View();
        }

    }
}
