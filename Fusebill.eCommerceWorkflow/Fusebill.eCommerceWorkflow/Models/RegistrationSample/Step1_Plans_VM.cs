using Fusebill.ApiWrapper.Dto.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Models
{
    public class Step1_Plans_VM
    {
        
        public List<Plan> AvailablePlans { get; set; }

        public long SelectedPlanID { get; set; }
    }
}