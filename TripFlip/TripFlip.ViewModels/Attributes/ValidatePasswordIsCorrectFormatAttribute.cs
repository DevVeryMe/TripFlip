using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TripFlip.ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidatePasswordIsCorrectFormatAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var isValid = false;

            if (value is string stringToValidate)
            {
                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasCorrectLength = new Regex(@"^.{8,50}$");

                isValid = hasNumber.IsMatch(stringToValidate) && 
                          hasUpperChar.IsMatch(stringToValidate) && 
                          hasCorrectLength.IsMatch(stringToValidate);
            }

            return isValid;
        }
    }
}
