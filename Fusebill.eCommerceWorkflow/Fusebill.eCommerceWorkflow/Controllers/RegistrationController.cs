using Fusebill.ApiWrapper;
using Fusebill.ApiWrapper.Dto.Get;
using Fusebill.eCommerceWorkflow.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fusebill.eCommerceWorkflow.Controllers
{
    public class RegistrationController : FusebillController
    {
        //
        // GET: /Registration/

        public ActionResult Index()
        {

            var testPlanIds = ConfigurationManager.AppSettings["TestPlanIds"];
            var testPlanArray = testPlanIds.Split(',');

            //This variable contains a list of all available plans
            var listOfPlans = ApiClient.GetPlans(new QueryOptions() { });


            //dictionary object for key value pair for each plan's ID and code
            var dictionaryOfIDsAndCodes = new Dictionary<long, string>();

            Step1_Plans_VM planList = new Step1_Plans_VM();
            planList.AvailablePlans = new List<Plan>();



            //place the key-value pairs into the dictionary object
            foreach (var plan in listOfPlans.Results)
            {
                if (testPlanArray.Contains(plan.Id.ToString()))
                {
                    planList.AvailablePlans.Add(plan);
                }
            }
            return View(planList);

        }

        [HttpPost]
        public ActionResult GetPlanProducts()
        {           
            Step2_Products_VM products = new Step2_Products_VM();
            products.SelectedPlanID = System.Convert.ToInt64(Request["planId"]);
            products.AvailableProduct = ApiClient.GetPlanProductsByPlanId(products.SelectedPlanID, new QueryOptions() { }).Results;
            return View(products);
        }

        public ActionResult GetCustomerDetails()
        {
            return View();
        }


    }
}
