using Fusebill.ApiWrapper;
using Fusebill.eCommerceWorkflow.Areas.ZampleZ.Models;
using Fusebill.eCommerceWorkflow.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Fusebill.eCommerceWorkflow.Areas.ZampleZ.Controllers
{
    public class SubscriptionsController : FusebillBaseController
    {

        // GET: /ZampleZ/Subscriptions/
        public ActionResult Index()
        {
            var demoCustomerIds = ConfigurationManager.AppSettings["DemoCustomerIds"].Split(',');

            var customersAndSubscriptionsVM = new CustomersAndSubscriptionsVM
            {
                AvailableCustomers = new List<ApiWrapper.Dto.Get.Customer>(),
                AvailablePlans = new List<ApiWrapper.Dto.Get.Plan>()
            };

            //add available customers
            foreach (var customerId in demoCustomerIds)
            {
                customersAndSubscriptionsVM.AvailableCustomers.Add(ApiClient.GetCustomer(Convert.ToInt64(customerId)));
            }

            //add availalable subscriptions
            var availablePlanIds = ConfigurationManager.AppSettings["DesiredPlanIds"];
            var availablePlans = availablePlanIds.Split(',');

            foreach (var plan in availablePlans)
            {
                customersAndSubscriptionsVM.AvailablePlans.Add(ApiClient.GetPlan(Convert.ToInt64(plan)));
            }

            return View(customersAndSubscriptionsVM);
        }

       
        [HttpPost]
        public ActionResult ListSubscriptionsForCustomer(PostCustomerIdVM postCustomerIdVM)
        {
            long desiredCustomerID = Convert.ToInt64(postCustomerIdVM.CustomerID);
            var subscriptions = ApiClient.GetSubscriptions(desiredCustomerID, new Fusebill.ApiWrapper.QueryOptions()).Results;

            return Json(subscriptions);
        }

        /// <summary>
        /// Updates/Edits a subscription
        /// </summary>
        /// <param name="postSubscriptionVM"></param>
        [HttpPost]
        public void UpdateSubscription(PostSubscriptionVM postSubscriptionVM)
        {
            var subscription = ApiClient.GetSubscription(postSubscriptionVM.SubscriptionID);

            for (int i = 0; i < subscription.SubscriptionProducts.Count; i++)
            {
                //If the user had inputed a plan product's quantity,  set it, otherwise do nothing
                if ( !String.IsNullOrEmpty(postSubscriptionVM.ProductQuantityOverrides[i]))
                    subscription.SubscriptionProducts[i].Quantity = Convert.ToInt32(postSubscriptionVM.ProductQuantityOverrides[i]);

                //If the user had inputed a plan product's price,  set it, otherwise, do nothing
                if (!String.IsNullOrEmpty(postSubscriptionVM.ProductPriceOverrides[i]))
                     subscription.SubscriptionProducts[i].SubscriptionProductPriceOverride = new ApiWrapper.Dto.Get.SubscriptionProductPriceOverride
                     {
                         ChargeAmount = Convert.ToDecimal(postSubscriptionVM.ProductPriceOverrides[i])
                     };
            }

            //Overriding a subscription's name and description
            subscription.SubscriptionOverride = new ApiWrapper.Dto.Get.SubscriptionOverride();
            if (!String.IsNullOrEmpty(postSubscriptionVM.NameOverride))   
                subscription.SubscriptionOverride.Name = postSubscriptionVM.NameOverride;  

            if (!String.IsNullOrEmpty(postSubscriptionVM.DescriptionOverride))
                subscription.SubscriptionOverride.Description = postSubscriptionVM.DescriptionOverride;


            //Editing the reference
            if (!String.IsNullOrEmpty(postSubscriptionVM.Reference))
                subscription.Reference = postSubscriptionVM.Reference;

            //Editing Contract Start and End Dates
            if (!String.IsNullOrEmpty(postSubscriptionVM.ContractStartTimestamp))
                subscription.ContractStartTimestamp = DateTime.Parse(postSubscriptionVM.ContractStartTimestamp);

            if (!String.IsNullOrEmpty(postSubscriptionVM.ContractEndTimestamp))
                subscription.ContractEndTimestamp = DateTime.Parse(postSubscriptionVM.ContractEndTimestamp);
  

            //Editing the Scheduled Activation Date
            if (!String.IsNullOrEmpty(postSubscriptionVM.ScheduledActivationTimestamp))
                subscription.ScheduledActivationTimestamp = DateTime.Parse(postSubscriptionVM.ScheduledActivationTimestamp);


            //Editing the expiration period. 
            if (!String.IsNullOrEmpty(postSubscriptionVM.RemainingInterval))
                subscription.RemainingInterval = Convert.ToInt32(postSubscriptionVM.RemainingInterval); 


            Automapping.SetupSubscriptionGetToPutMapping();
            var putSubscription = AutoMapper.Mapper.Map<Fusebill.ApiWrapper.Dto.Get.Subscription, Fusebill.ApiWrapper.Dto.Put.Subscription>(subscription);

            ApiClient.PutSubscription(putSubscription);
        }


        [HttpPost]
        /// <summary>
        /// To create a subscription, remember to specify the subscription and the target customer by using the planFrequencyID and the customerID fields.
        /// </summary>
        /// <returns></returns>
        public void CreateSubscription(CreateSubscriptionVM createSubscriptionVM)
        {
            var postSubscription = new ApiWrapper.Dto.Post.Subscription
            {
                CustomerId = Convert.ToInt64(createSubscriptionVM.CustomerID),
                PlanFrequencyId = createSubscriptionVM.PlanFrequencyID
            };

            ApiClient.PostSubscription(postSubscription);
        }


        [HttpPost]
        /// <summary>
        /// Deletes a subscription. Only draft and provisioned subscriptions can be deleted. Activated subscriptions can be cancelled.
        /// </summary>
        /// <returns></returns>
        public void DeleteSubscription(PostSubscriptionIdVM postSubscriptionIdVM)
        {
            ApiClient.DeleteSubscription(postSubscriptionIdVM.SubscriptionID);
        }


        [HttpPost]
        /// <summary>
        /// Provisioning a subscription requires the subscription's Id, the day and month the invoice is set. Only draft subscriptions with a Scheduled Activation Timestamp can be provisioned
        /// </summary>
        /// <returns></returns>
        public void ProvisionSubscription(PostSubscriptionIdVM postSubscriptionIdVM)
        {
            var postSubscriptionProvision = new Fusebill.ApiWrapper.Dto.Post.SubscriptionProvision
            {
                Id = postSubscriptionIdVM.SubscriptionID,
                InvoiceDay = Convert.ToInt32(postSubscriptionIdVM.InputValuesForActivationAndProvision[0]),
                InvoiceMonth = Convert.ToInt32(postSubscriptionIdVM.InputValuesForActivationAndProvision[0])
            };

            ApiClient.PostSubscriptionProvision(postSubscriptionProvision);
        }

        [HttpPost]
        /// <summary>
        /// Activating a subscription requires the subscription's Id, the day and month the invoice is set
        /// </summary>
        /// <param name="postSubscriptionIdVM"></param>
        /// <returns></returns>
        public ActionResult ActivateSubscription(PostSubscriptionIdVM postSubscriptionIdVM)
        {
            var postSubscriptionActivation = new Fusebill.ApiWrapper.Dto.Post.SubscriptionActivation
            {
                Id = postSubscriptionIdVM.SubscriptionID,
                InvoiceDay = Convert.ToInt32(postSubscriptionIdVM.InputValuesForActivationAndProvision[0]),
                InvoiceMonth = Convert.ToInt32(postSubscriptionIdVM.InputValuesForActivationAndProvision[1])
            };

            var returnedSubscription = ApiClient.PostSubscriptionActivation(postSubscriptionActivation);

            return Json(returnedSubscription);
        }


        [HttpPost]
        /// <summary>
        /// Cancelling an active subscription requires the id of the subscription to be cancelled and the cancellation options: "Full", "Unearned", "None". In this demo, "Full" is set
        /// </summary>
        /// <param name="postSubscriptionIdVM"></param>
        public void CancelSubscription(PostSubscriptionIdVM postSubscriptionIdVM)
        {
            var postSubscriptionCancel = new Fusebill.ApiWrapper.Dto.Post.SubscriptionCancel
            {
                SubscriptionId = postSubscriptionIdVM.SubscriptionID,
                CancellationOption = "Full"
            };

            ApiClient.PostSubscriptionCancel(postSubscriptionCancel);
        }
    }
}
