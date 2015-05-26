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
            var desiredPlanIds = ConfigurationManager.AppSettings["DesiredPlanIds"];

            //string array, each element is the ID of a desired plan
            var desiredPlans = desiredPlanIds.Split(',');

            Step1_Plans_VM planList = new Step1_Plans_VM();
            planList.AvailablePlans = new List<Plan>();

            
            foreach (string element in desiredPlans)
            {
                var plan = ApiClient.GetPlan(Convert.ToInt64(element));
                planList.AvailablePlans.Add(plan);
            }
            return View(planList);
        }











        [HttpPost]
        public ActionResult Step2_GetPlanProducts(Step1_Plans_VM step1_Plans_VM)
        {           
            
            Step2_Products_VM step2_Products_VM = new Step2_Products_VM();
            step2_Products_VM.SelectedPlanID = step1_Plans_VM.SelectedPlanID;
            step2_Products_VM.SelectedPlanName = ApiClient.GetPlan(step2_Products_VM.SelectedPlanID).Name;
            step2_Products_VM.AvailableProducts = ApiClient.GetPlanProductsByPlanId(step2_Products_VM.SelectedPlanID, new QueryOptions() { }).Results;


            step2_Products_VM.QuantitiesofPlanProducts = new Dictionary<int, decimal>();

            /*  foreach (var product in step2_Products_VM.AvailableProducts)
              {
                 //why does  step2_Products_VM.QuantitiesofPlanProducts.Add(product, product.Quantity); create two identical keys in the dictionary?

              }
              */

            step2_Products_VM.QuantitiesofPlanProducts_string = new Dictionary<string, decimal>();

            for (int i = 0; i < step2_Products_VM.AvailableProducts.Count; i++)
            {
                step2_Products_VM.QuantitiesofPlanProducts_string.Add(step2_Products_VM.AvailableProducts[i].ProductName, step2_Products_VM.AvailableProducts[i].Quantity);
            }

            step2_Products_VM.QuantitiesofPlanProducts[1234] = 23;
            return View(step2_Products_VM);
        }














        [HttpPost]
        public ActionResult Step3_GetCustomerDetails(Step2_Products_VM step2_Products_VM)
        {
            Step3_Customer_VM step3_Customer_VM = new Step3_Customer_VM();

            step3_Customer_VM.SelectedPlanID = step2_Products_VM.SelectedPlanID;



            step3_Customer_VM.SelectedPlanName = step2_Products_VM.SelectedPlanName;
            
            return View(step3_Customer_VM);
        }

        [HttpPost]
        public ActionResult Step4_GetPreviewInvoice(Step3_Customer_VM step3_Customer_VM)
        {
            Step4_PreviewInvoice_VM step4_PreviewInvoice_VM = new Step4_PreviewInvoice_VM();

            step4_PreviewInvoice_VM.SelectedPlanID = step3_Customer_VM.SelectedPlanID;

            step4_PreviewInvoice_VM.SelectedPlanName = step3_Customer_VM.SelectedPlanName;


          //  step4_PreviewInvoice_VM.customer = step3_Customer_VM.customer;

            #region GAHHHHH
            step4_PreviewInvoice_VM.Address1 = step3_Customer_VM.Address1;

            step4_PreviewInvoice_VM.Address2 = step3_Customer_VM.Address2;

            step4_PreviewInvoice_VM.AvailableProduct = step3_Customer_VM.AvailableProduct;

            step4_PreviewInvoice_VM.City = step3_Customer_VM.City;

            step4_PreviewInvoice_VM.CompanyName = step3_Customer_VM.CompanyName;

            step4_PreviewInvoice_VM.DefaultValue = step3_Customer_VM.DefaultValue;

            step4_PreviewInvoice_VM.Email = step3_Customer_VM.Email;

            step4_PreviewInvoice_VM.FirstName = step3_Customer_VM.FirstName;

            step4_PreviewInvoice_VM.LastName = step3_Customer_VM.LastName;

            step4_PreviewInvoice_VM.Mandatory = step3_Customer_VM.Mandatory;
            step4_PreviewInvoice_VM.Phone = step3_Customer_VM.Phone;
            step4_PreviewInvoice_VM.Usage = step3_Customer_VM.Usage;
            step4_PreviewInvoice_VM.ZipCode = step3_Customer_VM.ZipCode;


            #endregion

            


            return View(step4_PreviewInvoice_VM);
        }

        [HttpPost]
        public ActionResult Step5_GetCustomerCheckout(Step4_PreviewInvoice_VM step4_PreviewInvoice_VM)
        {
            Step5_CreatePayment_VM step5_CreatePayment = new Step5_CreatePayment_VM();
            return View();

        }


    }
}
