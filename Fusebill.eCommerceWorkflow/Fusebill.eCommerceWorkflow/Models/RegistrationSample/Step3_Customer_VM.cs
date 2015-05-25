using Fusebill.ApiWrapper.Dto.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Models
{
    public class Step3_Customer_VM
    {
        public int SelectedPlanID { get; set; }
        public List<PlanProduct> AvailableProduct { get; set; }


        #region CONTACT INFO

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string CompanyName { get; set; }

        public string Usage { get; set; }

        public string Mandatory { get; set; }

        public string DefaultValue { get; set; }

        #endregion
    }


}