using System;
using System.ComponentModel.DataAnnotations;
using Model;

namespace Fusebill.ApiWrapper.ValidationAttributes
{
    public class ChargeTimingTypeValidationAttribute : ValidationAttribute 
    {
        public ChargeTimingTypeValidationAttribute()
        {
            ErrorMessage = "Allowable Charge Timing types are: " + String.Join(", ", Enum.GetNames(typeof(Model.ChargeTimingType)));
        }

        public override bool IsValid(object value)
        {
            ChargeTimingType enumValue;

            return Enum.TryParse(value as string, out enumValue);
        }
    }
}
