using Fusebill.ApiWrapper.Dto.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Models
{
    public class Step4_PreviewInvoice_VM
    {
        public List<Plan> AvailablePlans { get; set; }

        //to change later
        public List<PlanProduct> AvailableProduct { get; set; }

        public long SelectedPlan { get; set; }

        public long CustomerID { get; set; }
    }

}