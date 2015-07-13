using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusebill.eCommerceWorkflow.Controllers;
using Fusebill.ApiWrapper;
using System.Configuration;
using Fusebill.eCommerceWorkflow.Areas.Samples.Models;

namespace Fusebill.eCommerceWorkflow.Areas.Samples.Controllers
{
    public class SubscriptionsController : FusebillBaseController
    {
        //
        // GET: /Samples/Subscriptions/

        public ActionResult Index()
        {
            var demoCustomerIds = ConfigurationManager.AppSettings["DemoCustomerIds"].Split(',');

            var listOfCustomersVM = new ListOfCustomersVM { Customers = new List<ApiWrapper.Dto.Get.Customer>() };

            foreach (var customerId in demoCustomerIds)
            {
                listOfCustomersVM.Customers.Add(ApiClient.GetCustomer(Convert.ToInt64(customerId)));
            }



            return View(listOfCustomersVM);
        }




        /// <summary>
        /// To create/post a subscription, we must specify which customer and which plan the subscription is for by using the customerID and planFrequencyID fields.
        /// </summary>
        /// <returns>ApiWrapper.Dto.Get.Subscription</returns>
        public ActionResult Create()
        {
            //var desiredPlansId = ConfigurationManager.AppSettings["DesiredPlanIds"];
            //var planIds = desiredPlansId.Split(',');
            //var plan = ApiClient.GetPlan(Convert.ToInt64(planIds[0]));

            //var postSubscription = new ApiWrapper.Dto.Post.Subscription
            //{
            //    CustomerId = Convert.ToInt64(ConfigurationManager.AppSettings["SubscriptionDemoCustomerID"]),
            //    PlanFrequencyId = plan.PlanFrequencies[0].Id
            //};

            // ApiClient.PostSubscription(postSubscription);
           

            return View();
        }

        //same to customer
        //YEAH, HOW DO I GET A SUBSCRIPTION??
        /// <summary>
        ///  This action will return a Subscription object that includes the full details of the Subscription. 
        ///  The subscription's ID is required to get the subscription
        /// </summary>
        /// <returns>ApiWrapper.Dto.Get.Subscription</returns>
        public ActionResult Get()
        {
            //var subscriptionID = Convert.ToInt64(ConfigurationManager.AppSettings["SubscriptionDemoSubscriptionID"]);
            //var subscription = ApiClient.GetSubscription(123);


            return View();
        }


        /// <summary>
        /// A Subscription object must already exist to put edit/put the subscription. Place the changes in the original Dto.Get.Subscription and then automap it to a Dto.Put.Subscription.
        /// </summary>
        /// <returns>ApiWrapper.Dto.Get.Subscription</returns>
        public ActionResult Edit()
        {
            //var subscriptionID = Convert.ToInt64(ConfigurationManager.AppSettings["SubscriptionDemoSubscriptionID"]);
            //var subscription = ApiClient.GetSubscription(subscriptionID);
           

            ////for (int i = 0; i < subscription. .AvailableProducts.Count; i++)
            ////{
            ////    //Editing the quantity of a plan product
            ////    var quantity = session.AvailableProducts[i].Quantity;
            ////    subscription.SubscriptionProducts[i].Quantity = quantity;
            ////    subscription.SubscriptionOverride.

            ////   //Editing a non-mandatory plan product to be included
            ////    var inclusion = session.AvailableProducts[i].IsIncluded;
            ////    subscription.SubscriptionProducts[i].IsIncluded = inclusion;
            ////}

            ////Editing a subscription's name, description, charge, setupFee, and ID  NOTE: To verify, uri is not included in the dto but is in the help.fusebill.com documentation. DOes API help mean "Sample" when it writes "Simple"?
            //subscription.SubscriptionOverride.Name = "New Override Sample Subscription Name";
            //subscription.SubscriptionOverride.Description = "New Override Sample Plan Description";
            //subscription.SubscriptionOverride.Charge = 1000.00M;
            //subscription.SubscriptionOverride.SetupFee = 150.00M;
            //subscription.SubscriptionOverride.Id = 142655;

            ////Editing the reference
            //subscription.Reference = "New Sample Reference";

            ////Editing Contract Start and End Dates
            //subscription.ContractStartTimestamp = new DateTime(2015, 5, 4);
            //subscription.ContractEndTimestamp = new DateTime(2016, 7, 6);

            ////Editing the Scheduled Activation Date
            //subscription.ScheduledActivationTimestamp = new DateTime(2015, 2, 3);

            ////Editing the expiration period NOTE: Typo : "This can SET BY submitting..."
            //subscription.RemainingInterval = 5; //setting the RemainingInterval property to 0 will result in an initial charge and then an immediate expiry of the subscription following activation.
            
            ///*
            //When modifying a subscription with discounts you are required to remove the subscriptionProductDiscount (legacy object) entity 
            //    and leave subscriptionProductDiscounts in place in order to carry forward your discounts.
            //Does this mean we set the subscriptionProductDiscount to null?*/
          

            //Automapping.SetupSubscriptionGetToPutMapping();
            //var putSubscription = AutoMapper.Mapper.Map<Fusebill.ApiWrapper.Dto.Get.Subscription, Fusebill.ApiWrapper.Dto.Put.Subscription>(subscription);

            //ApiClient.PutSubscription(putSubscription); 
            return View();
        }



        //WAIT, WHERE's THE DELETE DIRECTORY? I'm gonna assume for now that simply calling the ApiClient is sufficient for deleting a subscription
        /// <summary>
        /// For this demo, we first create a subscription and then delete it.
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {

            //var desiredPlansId = ConfigurationManager.AppSettings["DesiredPlanIds"];
            //var planIds = desiredPlansId.Split(',');
            //var plan = ApiClient.GetPlan(Convert.ToInt64(planIds[0]));

            //var postSubscription = new ApiWrapper.Dto.Post.Subscription
            //{
            //    CustomerId = Convert.ToInt64(ConfigurationManager.AppSettings["SubscriptionDemoCustomerID"]),
            //    PlanFrequencyId = plan.PlanFrequencies[0].Id
            //};

            //var subscription = ApiClient.PostSubscription(postSubscription);


            //ApiClient.DeleteSubscription(subscription.Id);
            return View();
        }

        //.  I think API meant to use "GetCustomer" instead of "GetCustomers". "GetCustomer" doesn't have the specified properties and neither does GetCustomers
        /// <summary>
        /// This action will return an array that contains a list of Subscriptions  applied against the specified Customer Id. 
        /// This call will return all Subscriptions regardless of status and will return an empty array if the Customer specified has no Subscriptions.
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            long customerID = Convert.ToInt64(ConfigurationManager.AppSettings["SubscriptionDemoCustomerID"]);
            var customer1 = ApiClient.GetCustomer(customerID);



            var customer2 = ApiClient.GetCustomers(new QueryOptions()).Results[0];
            //customer2.

            return View();
        }

        //The PostSubscriptionProvision takes an object, not a id
        /// <summary>
        /// 
        /// </summary>
        /// <returns>ApiWrapper.Dto.Get.Subscription</returns>
        public ActionResult Provision()
        {
            Fusebill.ApiWrapper.Dto.Post.SubscriptionProvision subscriptionProvision = new ApiWrapper.Dto.Post.SubscriptionProvision();
            subscriptionProvision.Id = Convert.ToInt64(ConfigurationManager.AppSettings["SubscriptionDemoSubscriptionID"]);
            subscriptionProvision.InvoiceDay = 15;
            subscriptionProvision.InvoiceMonth = 7;
            //billing period ID?

            ApiClient.PostSubscriptionProvision(subscriptionProvision);

            return View();
        }

        //POST SUBSCRIPTIONACTIVATIONEMAIL???? Where is postActivation
        /// <summary>
        /// This action will set the status of a Subscription to Active, which in most cases this will generate an invoice, and depending on the customer's billing options, apply a charge and a collection attempt against the Customer's payment method.
        /// Alternatively, you can call this call and pass in the "preview" field. 
        /// If "preview" is set to True then this will mimic the actual activation process and return a mock invoice which details the total value of this subscription. 
        /// You can then perform a POST /v1/Payments/ and if the payment is  successful you can then call this again without the "preview" field which actually activates the Subscription.
        /// </summary>
        /// <returns>ApiWrapper.Dto.Get.Subscription</returns>
        public ActionResult Activation()
        {
            Fusebill.ApiWrapper.Dto.Post.SubscriptionActivation subscriptionActivation = new ApiWrapper.Dto.Post.SubscriptionActivation();
            subscriptionActivation.Id = Convert.ToInt64(ConfigurationManager.AppSettings["SubscriptionDemoSubscriptionID"]);
            subscriptionActivation.InvoiceDay = 15;
             subscriptionActivation.InvoiceMonth = 7;
             //billing period ID?


            ApiClient.PostSubscriptionActivation(subscriptionActivation, preview: true);
            return View();
        }

        //I'm assuming that SUbscriptionCancellation is the same as SubscriptionCancel. Where the heck is the cancel method in the client.cs class???
        /// <summary>
        /// This action cancels an active Subscription, (only Subscriptions in Active status can be cancelled.)
        /// </summary>
        /// <returns>ApiWrapper.Dto.Get.Subscription</returns>
        public ActionResult Cancellation()
        {
            Fusebill.ApiWrapper.Dto.Post.SubscriptionCancel subscriptionCancel = new ApiWrapper.Dto.Post.SubscriptionCancel();
            subscriptionCancel.SubscriptionId = Convert.ToInt64(ConfigurationManager.AppSettings["SubscriptionDemoSubscriptionID"]);
            
            /*This field defines if the Customer receives a refund when the Subscription is canceled. Valid string are "None", "Unearned", "Full". 
             * None indicates the Customer will not receive a refund. 
             * Unearned indicates the customer will receive the Unearned revenue or Prorated Amount. 
             * Full indicates the customer will receive a full refund for the current billing period. 
             * */
            subscriptionCancel.CancellationOption = "Full";

            ApiClient.PostSubscriptionCancel(subscriptionCancel);
            return View();
        }

        //Cannot locate APiClient.postSubscriptionProductPriceOverride QQ
        /// <summary>
        /// This action creates a price override on a Subscription Product. 
        /// The override modifies the price which will be charge from that point forward when the product charges or quantity increases and is purchased.
        /// </summary>
        /// <returns></returns>
        public ActionResult CreatePriceOverride()
        {
            
            return View();
        }

        //Same issue as above
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPriceOverride()
        {

            return View();
        }

        //Uhhh, where is the delete folder in the core???
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult DeletePriceOverride()
        {
            return View();
        }

        public ActionResult ProductSearch()
        {
            return View();
        }
    }
}
