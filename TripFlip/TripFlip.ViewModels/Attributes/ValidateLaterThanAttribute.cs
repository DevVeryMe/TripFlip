using System;
using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class ValidateLaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparingDateTimePropertyName;

        public ValidateLaterThanAttribute(string comparingDateTimePropertyName)
        {
            _comparingDateTimePropertyName = comparingDateTimePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            DateTimeOffset? currentDateTime = value as DateTimeOffset?;

            var comparingDateTimeProperty = validationContext.ObjectType.GetProperty(_comparingDateTimePropertyName);

            if (comparingDateTimeProperty == null)
            {
                throw new ArgumentException("Property with this name not found.");
            }

            if (comparingDateTimeProperty.GetValue(validationContext.ObjectInstance) is 
                DateTimeOffset startsAtPropertyValue && currentDateTime != null)
            {
                return currentDateTime > startsAtPropertyValue ?
                     ValidationResult.Success :
                     new ValidationResult(ErrorMessage);
            }
                
            return ValidationResult.Success;
        }
    }
}
