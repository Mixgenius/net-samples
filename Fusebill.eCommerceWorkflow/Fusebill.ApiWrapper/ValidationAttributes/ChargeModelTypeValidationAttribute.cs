using System;
using System.ComponentModel.DataAnnotations;
using Model;

namespace Fusebill.ApiWrapper.ValidationAttributes
{
    public class ChargeModelTypeValidationAttribute : ValidationAttribute 
    {
        public ChargeModelTypeValidationAttribute()
        {
            ErrorMessage = "Allowable Charge Model types are: " + String.Join(", ", Enum.GetNames(typeof(Model.ChargeModelType)));
        }

        public override bool IsValid(object value)
        {
            ChargeModelType enumValue;

            return Enum.TryParse(value as string, out enumValue);
        }
    }
}
