using Fusebill.ApiWrapper;
using Fusebill.ApiWrapper.Dto.Get;
using Fusebill.eCommerceWorkflow.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication10.Models;

namespace Fusebill.eCommerceWorkflow.Controllers
{
    public class RegistrationController : FusebillController
    {
        //
        // GET: /Registration/

        public ActionResult Index()
        {
<<<<<<< HEAD
            var desiredPlanIds = ConfigurationManager.AppSettings["DesiredPlanIds"];

            //string array, each element is the ID of a desired plan
            var desiredPlans = desiredPlanIds.Split(',');

            Step1PlansVM planList = new Step1PlansVM();
            planList.AvailablePlans = new List<Plan>();

            
            foreach (string element in desiredPlans)
            {
                var plan = ApiClient.GetPlan(Convert.ToInt64(element));
                 planList.AvailablePlans.Add(plan);
            }
            return View(planList);
        }




        
        public ActionResult Step2GetPlanProducts(Step1PlansVM step1PlansVM)
        {           
            Step2ProductsVM step2ProductsVM = new Step2ProductsVM();
            step2ProductsVM.SelectedPlanID = step1PlansVM.SelectedPlanID;
            step2ProductsVM.SelectedPlanName = ApiClient.GetPlan(step2ProductsVM.SelectedPlanID).Name;
            step2ProductsVM.AvailableProducts = ApiClient.GetPlanProductsByPlanId(step2ProductsVM.SelectedPlanID, new QueryOptions() { }).Results;


            step2ProductsVM.QuantitiesofPlanProducts_string = new Dictionary<string, decimal>();

            step2ProductsVM.EmploymentType = new List<Check>(); 


            for (int i = 0; i < step2ProductsVM.AvailableProducts.Count; i++)
            {
                step2ProductsVM.QuantitiesofPlanProducts_string.Add(step2ProductsVM.AvailableProducts[i].ProductName, step2ProductsVM.AvailableProducts[i].Quantity);
                  step2ProductsVM.EmploymentType.Add(new Check {Text = i.ToString() });

            }

            return View(step2ProductsVM);
        }


        //ALSO STEP2, USED WHEN BACK BUTTON IS PRESSED
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step2GetPlanProducts")]
        public ActionResult Step2GetPlanProducts(Step3CustomerVM step3CustomerVM)
        {

            Step2ProductsVM step2ProductsVM = new Step2ProductsVM();
            step2ProductsVM.SelectedPlanID = step3CustomerVM.SelectedPlanID;
            step2ProductsVM.SelectedPlanName = ApiClient.GetPlan(step2ProductsVM.SelectedPlanID).Name;
            step2ProductsVM.AvailableProducts = ApiClient.GetPlanProductsByPlanId(step2ProductsVM.SelectedPlanID, new QueryOptions() { }).Results;

            #region GAHHHHH
            step2ProductsVM.Address1 = step3CustomerVM.Address1;
=======

            var testPlanIds = ConfigurationManager.AppSettings["TestPlanIds"];
            var testPlanArray = testPlanIds.Split(',');

            //This variable contains a list of all available plans
            var listOfPlans = ApiClient.GetPlans(new QueryOptions() { });
>>>>>>> parent of 46cfa4a... Changes to present

            step2ProductsVM.Address2 = step3CustomerVM.Address2;

<<<<<<< HEAD
            step2ProductsVM.City = step3CustomerVM.City;

            step2ProductsVM.CompanyName = step3CustomerVM.CompanyName;

            step2ProductsVM.DefaultValue = step3CustomerVM.DefaultValue;

            step2ProductsVM.Email = step3CustomerVM.Email;

            step2ProductsVM.FirstName = step3CustomerVM.FirstName;

            step2ProductsVM.LastName = step3CustomerVM.LastName;

            step2ProductsVM.Mandatory = step3CustomerVM.Mandatory;
            step2ProductsVM.Phone = step3CustomerVM.Phone;
            step2ProductsVM.Usage = step3CustomerVM.Usage;
            step2ProductsVM.ZipCode = step3CustomerVM.ZipCode;


            #endregion
            
            step2ProductsVM.QuantitiesofPlanProducts_string = new Dictionary<string, decimal>();

            step2ProductsVM.EmploymentType = new List<Check>();


            for (int i = 0; i < step2ProductsVM.AvailableProducts.Count; i++)
            {
                step2ProductsVM.QuantitiesofPlanProducts_string.Add(step2ProductsVM.AvailableProducts[i].ProductName, step2ProductsVM.AvailableProducts[i].Quantity);
                step2ProductsVM.EmploymentType.Add(new Check { Text = i.ToString() });

=======
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
>>>>>>> parent of 46cfa4a... Changes to present
            }
            return View(planList);

<<<<<<< HEAD
            return View(step2ProductsVM);
        }




        public ActionResult Step3GetCustomerDetails(Step2ProductsVM step2ProductsVM)
        {
            Customer cus = new Customer();
            
            Step3CustomerVM step3CustomerVM = new Step3CustomerVM();

            step3CustomerVM.EmploymentType = new List<Check>();

            step3CustomerVM.EmploymentType = step2ProductsVM.EmploymentType;

            step3CustomerVM.SelectedPlanID = step2ProductsVM.SelectedPlanID;



            step3CustomerVM.SelectedPlanName = step2ProductsVM.SelectedPlanName;

            return View(step3CustomerVM);
        }


        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step3GetCustomerDetails")]
        public ActionResult Step3GetCustomerDetails(Step4PreviewInvoiceVM step4PreviewInvoiceVM)
        {

            Step3CustomerVM step3CustomerVM = new Step3CustomerVM();

            step3CustomerVM.EmploymentType = new List<Check>();

            step3CustomerVM.EmploymentType = step4PreviewInvoiceVM.EmploymentType;

            step3CustomerVM.SelectedPlanID = step4PreviewInvoiceVM.SelectedPlanID;

            step3CustomerVM.SelectedPlanName = step4PreviewInvoiceVM.SelectedPlanName;

            #region GAHHHHH
            step4PreviewInvoiceVM.Address1 = step3CustomerVM.Address1;
            step4PreviewInvoiceVM.Address2 = step3CustomerVM.Address2;
            step4PreviewInvoiceVM.City = step3CustomerVM.City;
            step4PreviewInvoiceVM.CompanyName = step3CustomerVM.CompanyName;
            step4PreviewInvoiceVM.DefaultValue = step3CustomerVM.DefaultValue;
            step4PreviewInvoiceVM.Email = step3CustomerVM.Email;
            step4PreviewInvoiceVM.FirstName = step3CustomerVM.FirstName;
            step4PreviewInvoiceVM.LastName = step3CustomerVM.LastName;
            step4PreviewInvoiceVM.Mandatory = step3CustomerVM.Mandatory;
            step4PreviewInvoiceVM.Phone = step3CustomerVM.Phone;
            step4PreviewInvoiceVM.Usage = step3CustomerVM.Usage;
            step4PreviewInvoiceVM.ZipCode = step3CustomerVM.ZipCode;
            #endregion




            return View(step3CustomerVM);
=======
>>>>>>> parent of 46cfa4a... Changes to present
        }

  
   


        [HttpPost]
<<<<<<< HEAD
        [MultipleButton(Name = "action", Argument = "Step4GetPreviewInvoice")]
      
        public ActionResult Step4GetPreviewInvoice(Step3CustomerVM step3CustomerVM)
        {
            Step4PreviewInvoiceVM step4PreviewInvoiceVM = new Step4PreviewInvoiceVM();

            step4PreviewInvoiceVM.EmploymentType = new List<Check>();

            step4PreviewInvoiceVM.EmploymentType = step3CustomerVM.EmploymentType;


            step4PreviewInvoiceVM.SelectedPlanID = step3CustomerVM.SelectedPlanID;
            step4PreviewInvoiceVM.SelectedPlanName = step3CustomerVM.SelectedPlanName;

            step4PreviewInvoiceVM.AvailableProducts = ApiClient.GetPlanProductsByPlanId(step3CustomerVM.SelectedPlanID, new QueryOptions() { }).Results;


          //  step4PreviewInvoiceVM.customer = step3CustomerVM.customer;

            #region GAHHHHH
            step4PreviewInvoiceVM.Address1 = step3CustomerVM.Address1;

            step4PreviewInvoiceVM.Address2 = step3CustomerVM.Address2;

            step4PreviewInvoiceVM.City = step3CustomerVM.City;

            step4PreviewInvoiceVM.CompanyName = step3CustomerVM.CompanyName;

            step4PreviewInvoiceVM.DefaultValue = step3CustomerVM.DefaultValue;

            step4PreviewInvoiceVM.Email = step3CustomerVM.Email;

            step4PreviewInvoiceVM.FirstName = step3CustomerVM.FirstName;

            step4PreviewInvoiceVM.LastName = step3CustomerVM.LastName;

            step4PreviewInvoiceVM.Mandatory = step3CustomerVM.Mandatory;
            step4PreviewInvoiceVM.Phone = step3CustomerVM.Phone;
            step4PreviewInvoiceVM.Usage = step3CustomerVM.Usage;
            step4PreviewInvoiceVM.ZipCode = step3CustomerVM.ZipCode;


            #endregion
            return View(step4PreviewInvoiceVM);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step5GetCustomerCheckout")]
        public ActionResult Step5GetCustomerCheckout(Step4PreviewInvoiceVM step4PreviewInvoiceVM)
        {
            Step5CreatePaymentVM step5CreatePayment = new Step5CreatePaymentVM();

            if (step4PreviewInvoiceVM.IsSameAsBillingAddress)
            {
                step5CreatePayment.Address1 = step4PreviewInvoiceVM.Address1;
            }


            return View(step5CreatePayment);

=======
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
>>>>>>> parent of 46cfa4a... Changes to present
        }


    }
}
