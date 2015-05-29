using System;
using System.ComponentModel.DataAnnotations;
using Model;

namespace Fusebill.ApiWrapper.ValidationAttributes
{
    public class CurrencyStatusValidationAttribute : ValidationAttribute 
    {
        public CurrencyStatusValidationAttribute()
        {
            ErrorMessage = "Allowable Intervals are: " + String.Join(", ", Enum.GetNames(typeof(CurrencyStatus)));
        }

        public override bool IsValid(object value)
        {
            CurrencyStatus enumValue;

            return Enum.TryParse(value as string, out enumValue);
        }
    }
}
