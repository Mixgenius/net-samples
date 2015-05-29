using System;

namespace Model.Avalara
{
    // Request for address/validate is parsed into the URI query parameters.
    [Serializable]
    public class ValidateResult : BaseResult
    {
        public Address Address { get; set; }
    }
}
