using System;
using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class ValidateGreaterThanStartAt : ValidationAttribute
    {
        private readonly string _startsAt;

        public ValidateGreaterThanStartAt(string startsAt)
        {
            _startsAt = startsAt;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            DateTimeOffset? endsAt = value as DateTimeOffset?;

            var startsAtProperty = validationContext.ObjectType.GetProperty(_startsAt);

            if (startsAtProperty == null)
            {
                throw new ArgumentException("Property with this name not found");
            }

            if (startsAtProperty.GetValue(validationContext.ObjectInstance) is 
                DateTimeOffset startsAtPropertyValue && endsAt != null)
            {
                return endsAt > startsAtPropertyValue ? 
                    new ValidationResult(ErrorMessage) : 
                    ValidationResult.Success;
            }
                
            return ValidationResult.Success;
        }
    }
}
