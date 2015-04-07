using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fusebill.eCommerceWorkflow.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            // 1 - Get Plans
            // secure.fusebill.com/v1/plans/
            // 2 - Get Products for Plan
            // secure.fusebill.com/v1/plans/21334/planproducts/
            // 3 - Create a Customer
            // secure.fusebill.com/v1/Customers/
            // 4 - Create customer billing address
            // secure.fusebill.com/v1/addresses/
            // 5 - Create Subscription for Customer
            // secure.fusebill.com/v1/Subscriptions/
            // 6 - Edit Subscription and Include Optional Products
            // secure.fusebill.com/v1/Subscriptions/
            // 7 - Get Invoice Preview for Customer Activation
            // secure.fusebill.com/v1/CustomerActivation/?preview=true
            // 8 - Post Payment for Preview Total
            // secure.fusebill.com/v1/payments/
            // 9 - Activate Customer
            // secure.fusebill.com/v1/CustomerActivation/?preview=false

            return View();
        }
    }
}
