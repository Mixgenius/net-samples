using Fusebill.ApiWrapper.Dto.Get;
using Fusebill.eCommerceWorkflow.Models.RegistrationSample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Models
{
    public class Step3_Customer_VM
    {

        public string SelectedPlanName { get; set; }
        public long SelectedPlanID { get; set; }
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

        #region BillING INFO

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        #endregion



        //============================================= QUESTION


        public CustomerBillingInfo customer = new CustomerBillingInfo();


    }


}