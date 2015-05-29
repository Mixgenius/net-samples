using System;
using System.ComponentModel.DataAnnotations;
using Model;

namespace Fusebill.ApiWrapper.ValidationAttributes
{
    public class AddressTypeValidationAttribute : ValidationAttribute
    {
        public AddressTypeValidationAttribute()
        {
            ErrorMessage = "Allowable values are: " + String.Join(", ", Enum.GetNames(typeof(AddressType)));
        }

        public override bool IsValid(object value)
        {
            AddressType enumValue;

            return Enum.TryParse(value as string, out enumValue);
        }
    }
}
