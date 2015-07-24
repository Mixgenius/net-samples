using Fusebill.eCommerceWorkflow.Areas.Samples.Models;
using Fusebill.eCommerceWorkflow.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fusebill.eCommerceWorkflow.Areas.Samples.Controllers
{
    public class RefundController : FusebillBaseController
    {
        //
        // GET: /Samples/Refund/

        public ActionResult Index()
        {
            var x = ApiClient.GetCustomer(Convert.ToInt64(ConfigurationManager.AppSettings["DemoCustomerIds"].Split(',')[2]));
            var y = ApiClient.GetCustomers(new ApiWrapper.QueryOptions { QuickSearch = "Id" });

            return View();
        }

        [HttpPost]
        public ActionResult CreatePayment()
        {
            var postPayment = new Fusebill.ApiWrapper.Dto.Post.Payment();


            postPayment.CustomerId = Convert.ToInt64(ConfigurationManager.AppSettings["DemoCustomerIds"].Split(',')[2]);
                postPayment.Amount = 30;
                postPayment.Source = "Manual";
                postPayment.Reference = "Reference for payment";
                postPayment.PaymentMethodType = "Cash";

            var returnedPayment = ApiClient.PostPayment(postPayment);

            return Json(returnedPayment.Id);
        }

        [HttpPost]
        public void CreateRefund(RefundVM refundVM)
        {

            var postRefund = new Fusebill.ApiWrapper.Dto.Post.Refund();
            postRefund.Amount = refundVM.RefundAmount;
            postRefund.OriginalPaymentId = refundVM.Id;

            ApiClient.PostRefund(postRefund);
        }

    }
}
