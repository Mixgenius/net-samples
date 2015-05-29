using System.Collections.Generic;
using System.Linq;

namespace Model.FactoryMethods
{
    public class CustomerFactoryMethods
    {
        public static Customer GenerateSampleData()
        {
            var addresses = new List<Address>();
            var billingAddress = new Address
            {
                AddressType = AddressType.Billing,
                City = "Beverly Hills",
                CompanyName = "Vandelay Industries",
                Country = new Country { Name = "United States" },
                Line1 = "123 Downtown Street",
                Line2 = "Suite 101",
                PostalZip = "90210",
                State = new State { Name = "California" }
            };
            addresses.Add(billingAddress);
            var shippingAddress = new Address
            {
                AddressType = AddressType.Shipping,
                City = "Beverly Hills",
                CompanyName = "Vandelay Industries",
                Country = new Country { Name = "United States" },
                Line1 = "123 Downtown Street",
                Line2 = "Suite 101",
                PostalZip = "90210",
                State = new State { Name = "California" }
            };
            addresses.Add(shippingAddress);

            return new Customer
            {
                Id = 1,
                Reference = "a1",
                FirstName = "John",
                MiddleName = "Paul",
                LastName = "Doe",
                CompanyName = "Sample Company",
                Suffix = "Sr",
                Title = Title.Mr,
                PrimaryEmail = "john@doe.com",
                PrimaryPhone = "555-555-1234",
                SecondaryEmail = "jp@doe.com",
                SecondaryPhone = "555-555-5555",
                CustomerAddressPreference = new CustomerAddressPreference
                {
                    Addresses = addresses,
                    ShippingInstructions = "Leave parcel at the back",
                    ContactName = "John Doe",
                    UseBillingAddressAsShippingAddress = false
                },
                CurrencyId = 1,
                CustomerAccountStatusJournals = new List<CustomerAccountStatusJournal>
                {
                    new CustomerAccountStatusJournal { Status = CustomerAccountStatus.Good, IsActive = true }
                },
                CustomerStatusJournals = new List<CustomerStatusJournal>
                {
                new CustomerStatusJournal { Status = CustomerStatus.Active, IsActive = true }  
                },
                CustomerReference = new CustomerReference
                {
                    Reference1 = "Reference1",
                    Reference2 = "Reference2",
                    Reference3 = "Reference3",
                    SalesTrackingCode1 = CreateSalesTrackingCode(1),
                    SalesTrackingCode2 = CreateSalesTrackingCode(2),
                    SalesTrackingCode3 = CreateSalesTrackingCode(3),
                    SalesTrackingCode4 = CreateSalesTrackingCode(4),
                    SalesTrackingCode5 = CreateSalesTrackingCode(5)
                },
                CustomerAcquisition = new CustomerAcquisition
                {
                    AdContent = "AdContent",
                    Campaign = "Campaign",
                    Keyword = "Keyword",
                    LandingPage = "LandingPage",
                    Medium = "Medium",
                    Source = "Source"
                }
            };
        }

        private static SalesTrackingCode CreateSalesTrackingCode(int number)
        {
            return new SalesTrackingCode
            {
                Code = "SalesTrackingCode" + number + "Code",
                Email = "SalesTrackingCode" + number + "Email",
                Description = "SalesTrackingCode" + number + "Description",
                Name = "SalesTrackingCode" + number + "Name",
            };
        }

        public static CustomerCredentialWrapper GenerateCredentialSampleData()
        {
            return new CustomerCredentialWrapper
            {
                CustomerCredential = new CustomerCredential
                {
                    Id = 1,
                    Username = "JohnDoe"
                },
                PortalLink = "http://dummy.mybillsystem.com/Portal"
            };
        }

        public static CustomerEmailLog GenerateEmailLog(long customerId, string body, string subject, ICollection<CustomerEmailLogInvoice> customerEmailLogInvoices, ICollection<CustomerEmailLogBillingStatement> customerEmailLogBillingStatements, string bccEmail, string fromDisplayName, string fromEmail, string toDisplayName, string toEmail)
        {
            return new CustomerEmailLog
            {
                CustomerId = customerId,
                AttachmentIncluded = customerEmailLogInvoices.Any() || customerEmailLogBillingStatements.Any(),
                Body = body,
                Subject = subject,
                EffectiveTimestamp = null,
                CustomerEmailLogInvoices = customerEmailLogInvoices,
                CustomerEmailLogBillingStatement = customerEmailLogBillingStatements,
                BccEmail = bccEmail,
                FromDisplayName = fromDisplayName,
                FromEmail = fromEmail,
                ToDisplayName = toDisplayName,
                ToEmail = toEmail,
                Status = EmailStatus.Pending
            };
        }
    }
}
