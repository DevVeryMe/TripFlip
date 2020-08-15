using System;
using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class ValidateGreaterThanStartAt : ValidationAttribute
    {
        private readonly string _startsAtPropertyName;

        public ValidateGreaterThanStartAt(string startsAtPropertyName)
        {
            _startsAtPropertyName = startsAtPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            DateTimeOffset? endsAt = value as DateTimeOffset?;

            var startsAtProperty = validationContext.ObjectType.GetProperty(_startsAtPropertyName);

            if (startsAtProperty == null)
            {
                throw new ArgumentException("Property with this name not found");
            }

            if (startsAtProperty.GetValue(validationContext.ObjectInstance) is 
                DateTimeOffset startsAtPropertyValue && endsAt != null)
            {
                return endsAt > startsAtPropertyValue ?
                     ValidationResult.Success :
                     new ValidationResult(ErrorMessage);
            }
                
            return ValidationResult.Success;
        }
    }
}
