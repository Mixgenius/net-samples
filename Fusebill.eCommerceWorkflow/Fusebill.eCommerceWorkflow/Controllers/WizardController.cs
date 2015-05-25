using Fusebill.eCommerceWorkflow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fusebill.eCommerceWorkflow.Controllers
{
    public class WizardController : Controller
    {
        //
        // GET: /Wizard/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index(Wizard wiz)
        {
            if (ModelState.IsValid)
                return View("Complete", wiz);
            return View();
        }
    }
}
