using Fusebill.ApiWrapper.Dto.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Models
{
    public class Step5CreatePaymentVM
    {
<<<<<<< HEAD:Fusebill.eCommerceWorkflow/Fusebill.eCommerceWorkflow/Models/RegistrationSample/Step5CreatePaymentVM.cs
        public List<Check> EmploymentType { get; set; }

        public List<PlanProduct> AvailableProducts { get; set; }

        public long SelectedPlanID { get; set; }

        public string SelectedPlanName { get; set; }

        public long CustomerID { get; set; }

        public bool IsSameAsBillingAddress { get; set; }
=======
        public int SelectedPlanID { get; set; }
        public List<PlanProduct> AvailableProduct { get; set; }
>>>>>>> parent of 46cfa4a... Changes to present:Fusebill.eCommerceWorkflow/Fusebill.eCommerceWorkflow/Models/RegistrationSample/Step3_Customer_VM.cs


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
<<<<<<< HEAD:Fusebill.eCommerceWorkflow/Fusebill.eCommerceWorkflow/Models/RegistrationSample/Step5CreatePaymentVM.cs

        #region BillING INFO

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        #endregion



=======
>>>>>>> parent of 46cfa4a... Changes to present:Fusebill.eCommerceWorkflow/Fusebill.eCommerceWorkflow/Models/RegistrationSample/Step3_Customer_VM.cs
    }
}