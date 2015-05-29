using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.CreditCardValidation.Request
{
    public class BaseRequest
    {
        public int AccountId { get; set; }
        public bool Test { get; set; }
        public Dictionary<string, object> AdditionalAttributes { get; set; }
        public string OwnershipKey { get; set; }
        public string CurrencyIsoCode { get; set; }
    }
}
