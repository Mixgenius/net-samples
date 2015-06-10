using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fusebill.ApiWrapper.Dto.Get;
using Fusebill.eCommerceWorkflow.Models;

namespace Fusebill.eCommerceWorkflow.Common
{
    public static class RegistrationHelper
    {
        public static void SetCustomerAndBillingInformationToDefaults(RegistrationVM registration)
        {
            registration.BillingAddress = new Address
            {
                CompanyName = "Test Company",
                Line1 = "Line 1 Address",
                Line2 = "Line 2 Address",
                City = "Test City",
                PostalZip = "12345",

            };

            registration.CustomerInformation = new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                PrimaryEmail = "me@here.com",
                PrimaryPhone = "123-123-1234",
            };

            registration.CCAddressSameAsBilling = false;
        }

    }
}