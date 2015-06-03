using Fusebill.ApiWrapper;
using Fusebill.ApiWrapper.Dto.Get;
using Fusebill.eCommerceWorkflow.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

/*
 * Step3, in state dropdown: select your state for non-Canadian vs select your province for Canada
 * Step3, validation
 *  postBillingAddress.CustomerAddressPreferenceId, and how to find it
 * 
 * */


namespace Fusebill.eCommerceWorkflow.Controllers
{
    public class RegistrationController : FusebillController
    {

        //
        // GET: /Registration/

        public ActionResult Index()
        {
            RegistrationVM step1RegistrationVM = new RegistrationVM();

            var desiredPlanIds = ConfigurationManager.AppSettings["DesiredPlanIds"];

            //string array, each element is the ID of a desired plan
            var desiredPlans = desiredPlanIds.Split(',');

            step1RegistrationVM.AvailablePlans = new List<Plan>();
            step1RegistrationVM.AvailablePlanFrequencyIds = new List<long>();


            foreach (string element in desiredPlans)
            {
                var plan = ApiClient.GetPlan(Convert.ToInt64(element));
                step1RegistrationVM.AvailablePlans.Add(plan);
                step1RegistrationVM.AvailablePlanFrequencyIds.Add(plan.PlanFrequencies[0].Id);
            }

            return View(step1RegistrationVM);
        }


        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step2GetPlanProducts")]
        public ActionResult Step2GetPlanProducts(RegistrationVM registrationVM)
        {
            RegistrationVM step2RegistrationVM = new RegistrationVM();
            step2RegistrationVM.AvailableProducts = new List<PlanProduct>();

            //If selectedPlanId is 0, then its value is stored in the session. If it is not 0, its value is not yet stored so let's store it.
            if (registrationVM.SelectedPlanId != 0)
            {
                Session[new RegistrationStronglyTypedSessionState().selectedPlanId] = registrationVM.SelectedPlanId;
            }
            //same reasoning as above
            if (registrationVM.SelectedPlanFrequencyID != 0)
            {
                Session[new RegistrationStronglyTypedSessionState().selectedPlanFrequencyID] = registrationVM.SelectedPlanFrequencyID;
            }

            step2RegistrationVM.AvailableProducts = ApiClient.GetPlanProductsByPlanId(((long)Session[new RegistrationStronglyTypedSessionState().selectedPlanId]), new QueryOptions { }).Results;

            //the QuantityOfProducts instance will hold the number of each product the user chooses to obtain
            step2RegistrationVM.QuantityOfProducts = new Dictionary<string, decimal>();

            //if the session state containing the dictionary with the number of products exists, we'll use that to display the number of products.
            //if the session state DOESN'T EXIST, we'll use the original APIClient values

            if (Session[new RegistrationStronglyTypedSessionState().planProductQuantities] == null)
            {
                for (int i = 0; i < step2RegistrationVM.AvailableProducts.Count; i++)
                {
                    step2RegistrationVM.QuantityOfProducts.Add(step2RegistrationVM.AvailableProducts[i].ProductName, step2RegistrationVM.AvailableProducts[i].Quantity);
                }
            }
            else
            {
                step2RegistrationVM.QuantityOfProducts = ((Dictionary<string, decimal>)Session[new RegistrationStronglyTypedSessionState().planProductQuantities]);
            }

            step2RegistrationVM.planProductIncludes = new Dictionary<string, bool>();

            //If the session state containing the dictionary with the checkbox values, we'll use that to display the checkbox values.
            //if the session state DOESN'T EXIST, we'll set the checkbox values to false
            if (Session[new RegistrationStronglyTypedSessionState().planProductIncludes] == null)
            {
                for (int i = 0; i < step2RegistrationVM.AvailableProducts.Count; i++)
                {
                    step2RegistrationVM.planProductIncludes.Add(step2RegistrationVM.AvailableProducts[i].ProductName, false);
                }
            }
            else
            {
                step2RegistrationVM.planProductIncludes = ((Dictionary<string, bool>)Session[new RegistrationStronglyTypedSessionState().planProductIncludes]);
            }

            return View(step2RegistrationVM);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step3GetCustomerInformation")]
        public ActionResult Step3GetCustomerInformation(RegistrationVM registrationVM)
        {

            RegistrationVM step3RegistrationVM = new RegistrationVM();
            step3RegistrationVM = registrationVM;


            //store previous number of products and included values into session 
            Session[new RegistrationStronglyTypedSessionState().planProductQuantities] = step3RegistrationVM.QuantityOfProducts;
            //store previous value of checked checkboxes into session
            Session[new RegistrationStronglyTypedSessionState().planProductIncludes] = step3RegistrationVM.planProductIncludes;

            step3RegistrationVM.customerInformation = new Customer();
            step3RegistrationVM.billingAddress = new Address();



            //if the session holding customer information already exists, the TextBoxFors will display it. If not, to avoid a 
            //null exception error, we make the properties in the customer object empty strings
            if (Session[new RegistrationStronglyTypedSessionState().customerInformation] != null)
            {
                step3RegistrationVM.customerInformation = (Customer)Session[new RegistrationStronglyTypedSessionState().customerInformation];
            }
            else
            {
                step3RegistrationVM.customerInformation.FirstName = String.Empty;
                step3RegistrationVM.customerInformation.LastName = String.Empty;
                step3RegistrationVM.customerInformation.PrimaryEmail = String.Empty;
                step3RegistrationVM.customerInformation.PrimaryPhone = String.Empty;
            }

            //if the session holding billing information already exists, the TextBoxFors will display it. If not, to avoid a 
            //null exception error, we make the properties in the billing object empty strings
            if (Session[new RegistrationStronglyTypedSessionState().customerInformation] != null)
            {
                step3RegistrationVM.billingAddress = ((Address)Session[new RegistrationStronglyTypedSessionState().billingAddress]);
            }
            else
            {
                step3RegistrationVM.billingAddress.CompanyName = String.Empty;
                step3RegistrationVM.billingAddress.Line1 = String.Empty;
                step3RegistrationVM.billingAddress.Line2 = String.Empty;
                step3RegistrationVM.billingAddress.City = String.Empty;
                step3RegistrationVM.billingAddress.PostalZip = String.Empty;
            }

            //checking session for sameAsBilling checkbox
            if (Session[new RegistrationStronglyTypedSessionState().sameAsBilling] != null)
            {
                step3RegistrationVM.sameAsBilling = ((bool)Session[new RegistrationStronglyTypedSessionState().sameAsBilling]);
            }
            else
            {
                step3RegistrationVM.sameAsBilling = false;
            }


            step3RegistrationVM.listOfCountriesSLI = new List<SelectListItem>();
            step3RegistrationVM.listOfCountriesCountry = new List<Country>();
            step3RegistrationVM.listOfCountriesCountry = ApiClient.GetCountries();


            for (int i = 0; i < step3RegistrationVM.listOfCountriesCountry.Count; i++)
            {
                step3RegistrationVM.listOfCountriesSLI.Add(new SelectListItem
                {
                    Text = step3RegistrationVM.listOfCountriesCountry[i].Name,
                    Value = step3RegistrationVM.listOfCountriesCountry[i].Id.ToString(),
                });
            }

            return View(step3RegistrationVM);
        }

        //IS customer ID c.ID?

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step4GetInvoice")]
        public ActionResult Step4GetInvoice(RegistrationVM registrationVM)
        {
            Session[new RegistrationStronglyTypedSessionState().customerInformation] = registrationVM.customerInformation;
            Session[new RegistrationStronglyTypedSessionState().billingAddress] = registrationVM.billingAddress;
            Session[new RegistrationStronglyTypedSessionState().sameAsBilling] = registrationVM.sameAsBilling;

            RegistrationVM step4RegistrationVM = new RegistrationVM();
            step4RegistrationVM = registrationVM;

            step4RegistrationVM.subscription = new Subscription();


            #region                         Creating a Customer
            Fusebill.ApiWrapper.Dto.Post.Customer postCustomer = new Fusebill.ApiWrapper.Dto.Post.Customer();

            postCustomer.FirstName = step4RegistrationVM.customerInformation.FirstName;
            postCustomer.LastName = step4RegistrationVM.customerInformation.LastName;
            postCustomer.PrimaryEmail = step4RegistrationVM.customerInformation.PrimaryEmail;
            postCustomer.PrimaryPhone = step4RegistrationVM.customerInformation.PrimaryPhone;
            postCustomer.CompanyName = step4RegistrationVM.customerInformation.CompanyName;

            var c = ApiClient.PostCustomer(postCustomer);

            Fusebill.ApiWrapper.Dto.Post.CustomerActivation postCustomerActivation = new ApiWrapper.Dto.Post.CustomerActivation();
            postCustomerActivation.ActivateAllSubscriptions = true;
            postCustomerActivation.ActivateAllDraftPurchases = true;
            postCustomerActivation.CustomerId = c.Id;
            #endregion



            /*
            #region                         Creating an Address for billing

            Fusebill.ApiWrapper.Dto.Post.Address postBillingAddress = new Fusebill.ApiWrapper.Dto.Post.Address();

            postBillingAddress.CompanyName = step4RegistrationVM.billingAddress.CompanyName;
            postBillingAddress.Line1 = step4RegistrationVM.billingAddress.Line1;
            postBillingAddress.Line2 = step4RegistrationVM.billingAddress.Line2;
            postBillingAddress.City = step4RegistrationVM.billingAddress.City;
            postBillingAddress.PostalZip = step4RegistrationVM.billingAddress.PostalZip;
            postBillingAddress.CountryId = step4RegistrationVM.billingAddress.CountryId;
            postBillingAddress.StateId = step4RegistrationVM.billingAddress.StateId;
            postBillingAddress.CustomerAddressPreferenceId = 23;
            postBillingAddress.AddressType = "Billing";

            var ba = ApiClient.PostAddress(postBillingAddress);
            #endregion
            
            */


            #region                             Creating a Subscription
            Fusebill.ApiWrapper.Dto.Post.Subscription postSubscription = new ApiWrapper.Dto.Post.Subscription();

            
            postSubscription.CustomerId = c.Id;
            postSubscription.PlanFrequencyId = ((long)Session[new RegistrationStronglyTypedSessionState().selectedPlanFrequencyID]);
        
            var s = ApiClient.PostSubscription(postSubscription);
         
            //putting items into subscription

             step4RegistrationVM.AvailableProducts = ApiClient.GetPlanProductsByPlanId(((long)Session[new RegistrationStronglyTypedSessionState().selectedPlanId]), new QueryOptions() {}).Results;
           step4RegistrationVM.QuantityOfProducts = ( (Dictionary<string, decimal>) Session[new RegistrationStronglyTypedSessionState().planProductQuantities]);
            step4RegistrationVM.planProductIncludes = ( (Dictionary<string, bool>) Session[new RegistrationStronglyTypedSessionState().planProductIncludes]);

            for (int i = 0; i < step4RegistrationVM.AvailableProducts.Count; i++)
            {
                var product = step4RegistrationVM.QuantityOfProducts[    step4RegistrationVM.AvailableProducts[i].ProductName   ];
                var include = step4RegistrationVM.planProductIncludes[   step4RegistrationVM.AvailableProducts[i].ProductName   ];
                s.SubscriptionProducts[i].Quantity = product;
                s.SubscriptionProducts[i].IsIncluded = include;
            }



/*

            Fusebill.ApiWrapper.Dto.Put.Subscription putSubscription = new ApiWrapper.Dto.Put.Subscription();
            putSubscription.SubscriptionProducts = new List<ApiWrapper.Dto.Put.SubscriptionProduct>();
            putSubscription.Id = s.Id;
            
            
            for (int i = 0; i < s.SubscriptionProducts.Count; i++) 
            {
               Fusebill.ApiWrapper.Dto.Put.SubscriptionProduct putSubscriptionProduct = new ApiWrapper.Dto.Put.SubscriptionProduct();
               putSubscriptionProduct.IsIncluded = s.SubscriptionProducts[i].IsIncluded;
                putSubscriptionProduct.Quantity = s.SubscriptionProducts[i].Quantity;
                putSubscription.SubscriptionProducts.Add(putSubscriptionProduct);
            }

            ApiClient.PutSubscription(putSubscription);
*/
          
            Fusebill.ApiWrapper.Dto.Post.SubscriptionActivation subscriptionActivation = new ApiWrapper.Dto.Post.SubscriptionActivation();

            #endregion
           
            var pca = ApiClient.PostCustomerActivation(postCustomerActivation, true, true);

            //we make an instance of the returned customer to provide its values to the field
            step4RegistrationVM.postedCustomer = new Customer();
            step4RegistrationVM.postedCustomer = pca;

            Session[new RegistrationStronglyTypedSessionState().postCustomerActivation] = postCustomerActivation;

            return View(step4RegistrationVM);
        }





        [HttpGet]
        // [MultipleButton(Name = "action", Argument = "Step5GetPayment")]
        public ActionResult Step5GetPayment()
        {
            RegistrationVM step5RegistrationVM = new RegistrationVM();
            return View();
        }





        public ActionResult Step6GetActivation()
        {
            //first parameter is the customer object we made in the preview section.
            ApiClient.PostCustomerActivation( (Fusebill.ApiWrapper.Dto.Post.CustomerActivation)Session[new RegistrationStronglyTypedSessionState().postCustomerActivation] ,false, true);

            return View();
        }
    }
}










//        public ActionResult Step2GetPlanProducts(Step1PlansVM step1PlansVM)
//        {           
//            Step2ProductsVM step2ProductsVM = new Step2ProductsVM();
//            step2ProductsVM.SelectedPlanId = step1PlansVM.SelectedPlanId;
//            step2ProductsVM.SelectedPlanName = ApiClient.GetPlan(step2ProductsVM.SelectedPlanId).Name;
//            step2ProductsVM.AvailableProducts = ApiClient.GetPlanProductsByPlanId(step2ProductsVM.SelectedPlanId, new QueryOptions() { }).Results;


//            step2ProductsVM.QuantitiesofPlanProducts_string = new Dictionary<string, decimal>();

//            step2ProductsVM.EmploymentType = new List<Check>(); 


//            for (int i = 0; i < step2ProductsVM.AvailableProducts.Count; i++)
//            {
//                step2ProductsVM.QuantitiesofPlanProducts_string.Add(step2ProductsVM.AvailableProducts[i].ProductName, step2ProductsVM.AvailableProducts[i].Quantity);
//                  step2ProductsVM.EmploymentType.Add(new Check {Text = i.ToString() });

//            }

//            return View(step2ProductsVM);
//        }


//        ALSO STEP2, USED WHEN BACK BUTTON IS PRESSED
//        [HttpPost]
//        [MultipleButton(Name = "action", Argument = "Step2GetPlanProducts")]
//        public ActionResult Step2GetPlanProducts(Step3CustomerVM step3CustomerVM)
//        {

//            Step2ProductsVM step2ProductsVM = new Step2ProductsVM();
//            step2ProductsVM.SelectedPlanId = step3CustomerVM.SelectedPlanId;
//            step2ProductsVM.SelectedPlanName = ApiClient.GetPlan(step2ProductsVM.SelectedPlanId).Name;
//            step2ProductsVM.AvailableProducts = ApiClient.GetPlanProductsByPlanId(step2ProductsVM.SelectedPlanId, new QueryOptions() { }).Results;

//            #region GAHHHHH
//            step2ProductsVM.Address1 = step3CustomerVM.Address1;
//=======

//            var testPlanIds = ConfigurationManager.AppSettings["TestPlanIds"];
//            var testPlanArray = testPlanIds.Split(',');

//            This variable contains a list of all available plans
//            var listOfPlans = ApiClient.GetPlans(new QueryOptions() { });
//>>>>>>> parent of 46cfa4a... Changes to present

//            step2ProductsVM.Address2 = step3CustomerVM.Address2;

//<<<<<<< HEAD
//            step2ProductsVM.City = step3CustomerVM.City;

//            step2ProductsVM.CompanyName = step3CustomerVM.CompanyName;

//            step2ProductsVM.DefaultValue = step3CustomerVM.DefaultValue;

//            step2ProductsVM.Email = step3CustomerVM.Email;

//            step2ProductsVM.FirstName = step3CustomerVM.FirstName;

//            step2ProductsVM.LastName = step3CustomerVM.LastName;

//            step2ProductsVM.Mandatory = step3CustomerVM.Mandatory;
//            step2ProductsVM.Phone = step3CustomerVM.Phone;
//            step2ProductsVM.Usage = step3CustomerVM.Usage;
//            step2ProductsVM.ZipCode = step3CustomerVM.ZipCode;


//            #endregion

//            step2ProductsVM.QuantitiesofPlanProducts_string = new Dictionary<string, decimal>();

//            step2ProductsVM.EmploymentType = new List<Check>();


//            for (int i = 0; i < step2ProductsVM.AvailableProducts.Count; i++)
//            {
//                step2ProductsVM.QuantitiesofPlanProducts_string.Add(step2ProductsVM.AvailableProducts[i].ProductName, step2ProductsVM.AvailableProducts[i].Quantity);
//                step2ProductsVM.EmploymentType.Add(new Check { Text = i.ToString() });

//=======
//            dictionary object for key value pair for each plan's ID and code
//            var dictionaryOfIDsAndCodes = new Dictionary<long, string>();

//            Step1_Plans_VM planList = new Step1_Plans_VM();
//            planList.AvailablePlans = new List<Plan>();



//            place the key-value pairs into the dictionary object
//            foreach (var plan in listOfPlans.Results)
//            {
//                if (testPlanArray.Contains(plan.Id.ToString()))
//                {
//                    planList.AvailablePlans.Add(plan);
//                }
//>>>>>>> parent of 46cfa4a... Changes to present
//            }
//            return View(planList);

//<<<<<<< HEAD
//            return View(step2ProductsVM);
//        }




//        public ActionResult Step3GetCustomerDetails(Step2ProductsVM step2ProductsVM)
//        {
//            Customer cus = new Customer();

//            Step3CustomerVM step3CustomerVM = new Step3CustomerVM();

//            step3CustomerVM.EmploymentType = new List<Check>();

//            step3CustomerVM.EmploymentType = step2ProductsVM.EmploymentType;

//            step3CustomerVM.SelectedPlanId = step2ProductsVM.SelectedPlanId;



//            step3CustomerVM.SelectedPlanName = step2ProductsVM.SelectedPlanName;

//            return View(step3CustomerVM);
//        }


//        [HttpPost]
//        [MultipleButton(Name = "action", Argument = "Step3GetCustomerDetails")]
//        public ActionResult Step3GetCustomerDetails(Step4PreviewInvoiceVM step4PreviewInvoiceVM)
//        {

//            Step3CustomerVM step3CustomerVM = new Step3CustomerVM();

//            step3CustomerVM.EmploymentType = new List<Check>();

//            step3CustomerVM.EmploymentType = step4PreviewInvoiceVM.EmploymentType;

//            step3CustomerVM.SelectedPlanId = step4PreviewInvoiceVM.SelectedPlanId;

//            step3CustomerVM.SelectedPlanName = step4PreviewInvoiceVM.SelectedPlanName;

//            #region GAHHHHH
//            step4PreviewInvoiceVM.Address1 = step3CustomerVM.Address1;
//            step4PreviewInvoiceVM.Address2 = step3CustomerVM.Address2;
//            step4PreviewInvoiceVM.City = step3CustomerVM.City;
//            step4PreviewInvoiceVM.CompanyName = step3CustomerVM.CompanyName;
//            step4PreviewInvoiceVM.DefaultValue = step3CustomerVM.DefaultValue;
//            step4PreviewInvoiceVM.Email = step3CustomerVM.Email;
//            step4PreviewInvoiceVM.FirstName = step3CustomerVM.FirstName;
//            step4PreviewInvoiceVM.LastName = step3CustomerVM.LastName;
//            step4PreviewInvoiceVM.Mandatory = step3CustomerVM.Mandatory;
//            step4PreviewInvoiceVM.Phone = step3CustomerVM.Phone;
//            step4PreviewInvoiceVM.Usage = step3CustomerVM.Usage;
//            step4PreviewInvoiceVM.ZipCode = step3CustomerVM.ZipCode;
//            #endregion




//            return View(step3CustomerVM);
//=======
//>>>>>>> parent of 46cfa4a... Changes to present
//        }





//        [HttpPost]
//<<<<<<< HEAD
//        [MultipleButton(Name = "action", Argument = "Step4GetPreviewInvoice")]

//        public ActionResult Step4GetPreviewInvoice(Step3CustomerVM step3CustomerVM)
//        {
//            Step4PreviewInvoiceVM step4PreviewInvoiceVM = new Step4PreviewInvoiceVM();

//            step4PreviewInvoiceVM.EmploymentType = new List<Check>();

//            step4PreviewInvoiceVM.EmploymentType = step3CustomerVM.EmploymentType;


//            step4PreviewInvoiceVM.SelectedPlanId = step3CustomerVM.SelectedPlanId;
//            step4PreviewInvoiceVM.SelectedPlanName = step3CustomerVM.SelectedPlanName;

//            step4PreviewInvoiceVM.AvailableProducts = ApiClient.GetPlanProductsByPlanId(step3CustomerVM.SelectedPlanId, new QueryOptions() { }).Results;


//            step4PreviewInvoiceVM.customer = step3CustomerVM.customer;

//            #region GAHHHHH
//            step4PreviewInvoiceVM.Address1 = step3CustomerVM.Address1;

//            step4PreviewInvoiceVM.Address2 = step3CustomerVM.Address2;

//            step4PreviewInvoiceVM.City = step3CustomerVM.City;

//            step4PreviewInvoiceVM.CompanyName = step3CustomerVM.CompanyName;

//            step4PreviewInvoiceVM.DefaultValue = step3CustomerVM.DefaultValue;

//            step4PreviewInvoiceVM.Email = step3CustomerVM.Email;

//            step4PreviewInvoiceVM.FirstName = step3CustomerVM.FirstName;

//            step4PreviewInvoiceVM.LastName = step3CustomerVM.LastName;

//            step4PreviewInvoiceVM.Mandatory = step3CustomerVM.Mandatory;
//            step4PreviewInvoiceVM.Phone = step3CustomerVM.Phone;
//            step4PreviewInvoiceVM.Usage = step3CustomerVM.Usage;
//            step4PreviewInvoiceVM.ZipCode = step3CustomerVM.ZipCode;


//            #endregion
//            return View(step4PreviewInvoiceVM);
//        }

//        [HttpPost]
//        [MultipleButton(Name = "action", Argument = "Step5GetCustomerCheckout")]
//        public ActionResult Step5GetCustomerCheckout(Step4PreviewInvoiceVM step4PreviewInvoiceVM)
//        {
//            Step5CreatePaymentVM step5CreatePayment = new Step5CreatePaymentVM();

//            if (step4PreviewInvoiceVM.IsSameAsBillingAddress)
//            {
//                step5CreatePayment.Address1 = step4PreviewInvoiceVM.Address1;
//            }


//            return View(step5CreatePayment);

//=======
//        public ActionResult GetPlanProducts()
//        {           
//            Step2_Products_VM products = new Step2_Products_VM();
//            products.SelectedPlanId = System.Convert.ToInt64(Request["planId"]);
//            products.AvailableProduct = ApiClient.GetPlanProductsByPlanId(products.SelectedPlanId, new QueryOptions() { }).Results;
//            return View(products);
//        }

//        public ActionResult GetCustomerDetails()
//        {
//            return View();
//>>>>>>> parent of 46cfa4a... Changes to present
//        }


//    }
//}
