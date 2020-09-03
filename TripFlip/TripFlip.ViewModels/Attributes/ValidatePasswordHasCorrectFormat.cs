using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TripFlip.ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidatePasswordHasCorrectFormat : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool isValid = false;
            
            if (value is string stringToValidate)
            {
                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasSpecialSymbol = new Regex(@"[#?!@$%^&*-]+");
                var hasCorrectLength = new Regex(@"^.{8,100}$");
                
                isValid = hasNumber.IsMatch(stringToValidate) && 
                    hasUpperChar.IsMatch(stringToValidate) && 
                    hasCorrectLength.IsMatch(stringToValidate) &&
                    hasSpecialSymbol.IsMatch(stringToValidate);
            }
            
            return isValid;
        }
    }
}
