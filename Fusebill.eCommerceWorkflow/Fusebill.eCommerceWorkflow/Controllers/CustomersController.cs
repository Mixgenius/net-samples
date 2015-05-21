using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusebill.ApiWrapper;

namespace Fusebill.eCommerceWorkflow.Controllers
{
    public class CustomersController : FusebillController
    {
        //
        // GET: /Customers/

        public ActionResult Index()
        {
            return Content("Nothing to show here!");
        }


        public ActionResult Search(long id)
        {
            var customer = ApiClient.GetCustomer(id);
            return View();
        }

    }
}
