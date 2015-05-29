using Fusebill.ApiWrapper.Dto.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Models
{
    public class Step4PreviewInvoiceVM
    {
<<<<<<< HEAD:Fusebill.eCommerceWorkflow/Fusebill.eCommerceWorkflow/Models/RegistrationSample/Step4PreviewInvoiceVM.cs
        public List<Check> EmploymentType { get; set; }

        public List<PlanProduct> AvailableProducts { get; set; }
=======
        public List<Plan> AvailablePlans { get; set; }
>>>>>>> parent of 46cfa4a... Changes to present:Fusebill.eCommerceWorkflow/Fusebill.eCommerceWorkflow/Models/RegistrationSample/Step4_PreviewInvoice_VM.cs

        //to change later
        public List<PlanProduct> AvailableProduct { get; set; }

        public long SelectedPlan { get; set; }

        public long CustomerID { get; set; }
<<<<<<< HEAD:Fusebill.eCommerceWorkflow/Fusebill.eCommerceWorkflow/Models/RegistrationSample/Step4PreviewInvoiceVM.cs

        public bool IsSameAsBillingAddress { get; set; }


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

        //===========================================





=======
>>>>>>> parent of 46cfa4a... Changes to present:Fusebill.eCommerceWorkflow/Fusebill.eCommerceWorkflow/Models/RegistrationSample/Step4_PreviewInvoice_VM.cs
    }

}