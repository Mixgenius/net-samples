using System;
using System.ComponentModel.DataAnnotations;
using Model;

namespace Fusebill.ApiWrapper.ValidationAttributes
{
    public class CustomerAgingOptionValidationAttribute : ValidationAttribute
    {
        public CustomerAgingOptionValidationAttribute()
        {
            ErrorMessage = "Allowable values are: " + String.Join(", ", Enum.GetNames(typeof(CustomerAgingOption)));
        }

        public override bool IsValid(object value)
        {
            CustomerAgingOption enumValue;

            return Enum.TryParse(value as string, out enumValue);
        }
    }
}
