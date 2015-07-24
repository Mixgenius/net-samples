using Fusebill.eCommerceWorkflow.Areas.Samples.Models;
using Fusebill.eCommerceWorkflow.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fusebill.eCommerceWorkflow.Areas.Samples.Controllers
{
    public class ReversalController : FusebillBaseController
    {
        
        // GET: /Samples/Reversal/

        public ActionResult Index()
        {
            var customerID = 4622859;
            var planFrequencyID = 10802244;

            List<decimal> invoiceCharges = new List<decimal>();

            var invoices = ApiClient.GetInvoicesByCustomerId(customerID, new ApiWrapper.QueryOptions()).Results;
            ReversalVM reversalController = new ReversalVM { invoices = new List<ApiWrapper.Dto.Get.Invoice>() };

            reversalController.invoices = invoices;

            return View(reversalController);
        }

        public void ReverseCharge(ReversalDetailsVM reversalDetailsVM)
        {
            //Reversed amount is greater than the remaining charge amount reversal.

            var postReverseCharge = new Fusebill.ApiWrapper.Dto.Post.ReverseCharge
            {
                ChargeId = reversalDetailsVM.invoiceID,
                ReverseChargeOption = reversalDetailsVM.reverseOption,
                Reference = reversalDetailsVM.reference
            };

            var x = ApiClient.GetInvoicesByCustomerId(4622859, new ApiWrapper.QueryOptions()).Results[0].Charges[0].Id;

            if (reversalDetailsVM.reverseOption.Equals("Amount"))
            {
                postReverseCharge.ReverseChargeAmount = reversalDetailsVM.specificAmount;
            }


            ApiClient.PostReverseCharge(postReverseCharge);
        }

    }
}
